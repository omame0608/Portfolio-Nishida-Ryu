using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// マウスによってカメラを操作するクラス
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    //フィールド
    [Header("カメラの移動スピード")]
    [SerializeField] private float _moveSpeed = 0.02f;
    [Header("カメラのズームスピード")]
    [SerializeField] private float _zoomSpeed = 0.5f;

    //カメラの座標範囲
    private float _MIN_X; //x座標最小値
    private float _MAX_X; //x座標最大値
    private float _MIN_Y; //y座標最小値
    private float _MAX_Y; //y座標最大値

    //カメラのズームの範囲
    private const float _MIN_ZOOM_SIZE = 2.0f;  //限界ズームイン時のサイズ
    private const float _MAX_ZOOM_SIZE = 20.0f; //限界ズームアウト時のサイズ

    //マウスの座標情報
    private Vector3 _mouseStandardPos; //マウスの基準座標
    private Vector3 _mouseDeltaPos;    //マウスの差分ベクトル

    //カメラの座標情報
    private Vector3 _cameraStandardPos; //カメラの基準座標
    private Vector3 _cameraDeltaPos;    //カメラの差分ベクトル

    //参照一覧
    [SerializeField] private SpriteRenderer _backGround; //背景

    //キャッシュ一覧
    private Camera _camera; //カメラ


    void Start()
    {
        //背景の大きさに応じて動的にカメラの移動範囲を設定する
        _MIN_X = -_backGround.size.x / 2;
        _MAX_X =  _backGround.size.x / 2;
        _MIN_Y = -_backGround.size.y / 2;
        _MAX_Y =  _backGround.size.y / 2;

        //キャッシュの取得
        _camera = GetComponent<Camera>();
    }


    void Update()
    {
        //マウスホイールが押されたら各種基準座標を設定する
        if (Input.GetMouseButtonDown((int)MouseButton.Middle))
        {
            _mouseStandardPos = Input.mousePosition;
            _cameraStandardPos = transform.position;
        }

        //マウスホイール長押し中は各種差分ベクトルを作成し逐次カメラの座標に適用する
        else if (Input.GetMouseButton((int)MouseButton.Middle))
        {
            //差分ベクトルを作成
            _mouseDeltaPos = Input.mousePosition - _mouseStandardPos;
            _cameraDeltaPos = -_mouseDeltaPos * _moveSpeed;

            //移動後のカメラの座標を作成
            Vector3 nextCameraPos = _cameraStandardPos + _cameraDeltaPos;

            //移動後のカメラの座標が範囲内に収まるように加工する
            if (nextCameraPos.x < _MIN_X) nextCameraPos.x = _MIN_X;
            if (nextCameraPos.x > _MAX_X) nextCameraPos.x = _MAX_X;
            if (nextCameraPos.y < _MIN_Y) nextCameraPos.y = _MIN_Y;
            if (nextCameraPos.y > _MAX_Y) nextCameraPos.y = _MAX_Y;

            //カメラを移動
            transform.position = nextCameraPos;
        }

        //マウスホイールがスクロールされたらズームイン・ズームアウトをする
        float mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollValue != 0f)
        {
            //変更後に範囲を超えていなければズームを適用する
            if (mouseScrollValue > 0f && _camera.orthographicSize - _zoomSpeed >= _MIN_ZOOM_SIZE)
            {
                _camera.orthographicSize -= _zoomSpeed; //ズームイン
            }
            if (mouseScrollValue < 0f && _camera.orthographicSize + _zoomSpeed <= _MAX_ZOOM_SIZE)
            {
                _camera.orthographicSize += _zoomSpeed; //ズームアウト
            }
        }
    }
}
