using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用：カメラ操作用(開発用操作)
/// </summary>
public class TestCameraController : MonoBehaviour
{
    //フィールド
    [Header("移動スピード")]
    [SerializeField] private float _moveSpeed;
    [Header("マウス感度")]
    [SerializeField] private float _mouseSensitivity;

    //操作用
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    [SerializeField] private Transform _topTransform;

    //テスト中
    private Rigidbody _topRigidbody;


    void Start()
    {
        //カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        //視点を正面に初期化
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        //テスト中
        _topRigidbody = _topTransform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //カメラの向きを変更
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _yRotation += mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);

        //カメラを移動
        float moveX = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        //_topTransform.position += move;
        //_topRigidbody.MovePosition(_topRigidbody.position + move);
        _topRigidbody.velocity = move * 400;


        //左クリックでオブジェクトを選択
        if (Input.GetMouseButtonDown(0))
        {
            //レイを作成
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //レイを飛ばす
            if (Physics.Raycast(ray, out hit))
            {
                //IActionableなら処理を委譲
                IActionable selectObj = hit.collider.GetComponent<IActionable>();
                if (selectObj == null) return;
                selectObj.UserAction();
            }
        }
    }
}