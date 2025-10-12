using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

/// <summary>
/// ドラッグによってラインを表示する
/// 特定の操作でConnectionを生成する
/// </summary>
public class ConnectionLiner : MonoBehaviour
{
    //フィールド
    private float _lineSize = 0.1f;

    //操作用
    private LineRenderer _lineRenderer;
    private Vector3 _startPos;
    private InputPort _inputPort;
    private OutputPort _outputPort;

    //キャッシュ一覧
    private Camera _camera; //カメラ

    //参照
    [Inject] private IConnectionFactory _connectionFactory;


    void Start()
    {
        //キャッシュの取得
        _camera = Camera.main;
    }


    void OnMouseDown()
    {
        //ドラッグ開始時にラインを生成・初期化する
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = _lineSize;
        _lineRenderer.endWidth = _lineSize;
        _lineRenderer.material.color = new Color(1f, 1f, 1f, 0.1f);
        _startPos = gameObject.transform.position;
        _startPos.z = -2f;

        //現在のカーソルの位置
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -2f;

        //レイを飛ばしてラインの始点を判定
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            //in => の場合
            if (hit.collider.TryGetComponent<InputPort>(out var inputPort))
            {
                _inputPort = inputPort;
                _outputPort = null;
            }
            //out => の場合
            else if (hit.collider.TryGetComponent<OutputPort>(out var outputPort))
            {
                _inputPort = null;
                _outputPort = outputPort;
            }
        }
    }


    void OnMouseDrag()
    {
        //現在のカーソルの位置
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -2f;

        //ラインの始点・終点を作成
        var positions = new Vector3[] { _startPos, mousePos };

        // 線を引く場所を指定する
        _lineRenderer.SetPositions(positions);
    }


    void OnMouseUp()
    {
        //ドラッグ終了時のカーソルの位置
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -2f;

        //レイを飛ばしてオブジェクトを判定
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            //in => out の場合
            if (_outputPort == null && hit.collider.TryGetComponent<OutputPort>(out var outputPort))
            {
                _outputPort = outputPort;
                CheckOnFactory();
            }
            //out => in の場合
            else if (_inputPort == null && hit.collider.TryGetComponent<InputPort>(out var inputPort))
            {
                _inputPort = inputPort;
                CheckOnFactory();
            }
        }

        Destroy(_lineRenderer);
    }


    /// <summary>
    /// connectionの生成または削除をFactory経由で行う
    /// </summary>
    private void CheckOnFactory()
    {
        if (_connectionFactory.JudgeConnectOrDispose(_inputPort, _outputPort))
        {
            if (_inputPort.InputConnection.Value == null) _connectionFactory.Create(_inputPort, _outputPort);
        }
        else
        {
            _connectionFactory.Dispose(_inputPort, _outputPort);
        }
    }
}
