using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 左クリックでドラッグするとオブジェクトを動かすことができる
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DragMover : MonoBehaviour
{
    //フィールド
    [Header("x方向の移動可能範囲縮小値")]
    [SerializeField] private float _moveBorderX;
    [Header("y方向の移動可能範囲縮小値")]
    [SerializeField] private float _moveBorderY;

    //対象の座標範囲
    private float _MIN_X; //x座標最小値
    private float _MAX_X; //x座標最大値
    private float _MIN_Y; //y座標最小値
    private float _MAX_Y; //y座標最大値

    //操作用
    private Vector3 _offset; //中心からクリックした場所までの差

    //参照用
    private SpriteRenderer _backGround; //背景
    [SerializeField] private List<InputPort> _inputPorts = new List<InputPort>();
    [SerializeField] private OutputPort _outputPort;

    //キャッシュ一覧
    private Camera _camera; //カメラ


    void Start()
    {
        //背景を取得
        _backGround = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        if (_backGround == null) Debug.LogError("背景が見つかりません");

        //背景の大きさに応じて動的に対象の移動範囲を設定する
        _MIN_X = -_backGround.size.x / 2 + _moveBorderX;
        _MAX_X =  _backGround.size.x / 2 - _moveBorderX;
        _MIN_Y = -_backGround.size.y / 2 + _moveBorderY;
        _MAX_Y =  _backGround.size.y / 2 - _moveBorderY;

        //キャッシュの取得
        _camera = Camera.main;
    }


    void OnMouseDown()
    {
        //オブジェクトの中心からクリックした場所までの差を取得
        _offset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
    }


    void OnMouseDrag()
    {
        //移動後の座標を作成
        Vector3 nextPos = _camera.ScreenToWorldPoint(Input.mousePosition) + _offset;

        //移動後の座標が範囲内に収まるように加工する
        if (nextPos.x < _MIN_X) nextPos.x = _MIN_X;
        if (nextPos.x > _MAX_X) nextPos.x = _MAX_X;
        if (nextPos.y < _MIN_Y) nextPos.y = _MIN_Y;
        if (nextPos.y > _MAX_Y) nextPos.y = _MAX_Y;

        //対象を移動
        transform.position = nextPos;

        //対象にConnectionがある場合それらも移動する
        //inputPortに接続がある場合
        foreach (var port in _inputPorts)
        {
            var movedPos = port.transform.position;
            movedPos.z = -2f;
            if (port.InputConnection.Value != null)
            {
                port.InputConnection.Value.LineBody?.SetPosition(0, movedPos);
            }
        }
        //outputPortに接続がある場合
        if (_outputPort != null)
        {
            var movedPos = _outputPort.transform.position;
            movedPos.z = -2f;
            _outputPort.OutputConnections.ForEach(connection =>
            {
                connection.LineBody?.SetPosition(1, movedPos);
            });
        }
    }


    void OnMouseUp()
    {
        //RaycastAllの引数（PointerEventData）作成
        PointerEventData pointData = new PointerEventData(EventSystem.current);

        //RaycastAllの結果格納用List
        List<RaycastResult> RayResult = new List<RaycastResult>();

        //PointerEventDataにマウスの位置をセット
        pointData.position = Input.mousePosition;
        //RayCast（スクリーン座標）
        EventSystem.current.RaycastAll(pointData, RayResult);

        foreach (RaycastResult result in RayResult)
        {
            //ゴミ箱UIの上でドロップされたらノードを削除できるか判定する
            if (result.gameObject.GetComponentInParent<Garbage>() != null)
            {
                //ノードに繋がっているconnectionの数をカウント
                int c = 0;
                foreach (var inputPort in _inputPorts)
                {
                    if (inputPort.InputConnection.Value != null) c++;
                }
                if (_outputPort != null) c += _outputPort.OutputConnections.Count;

                //connectionが無ければノードを削除できる
                if (c == 0)
                {
                    Debug.Log($"ノードを削除します");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log($"connectionが繋がっているため削除できません");
                }
            }
        }
    }
}
