using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//右コントローラのスティックではカメラの移動を制御
public class LeftControllerStick : MonoBehaviour
{
    private Transform cameraT;
    private Rigidbody cameraRb;
    private Transform duration;
    private OVRInput.Controller controller;
    [SerializeField, Range(0f, 1f)]
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cameraT = GameObject.Find("[BuildingBlock] Camera Rig").GetComponent<Transform>();
        cameraRb = GameObject.Find("[BuildingBlock] Camera Rig").GetComponent<Rigidbody>();
        duration = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>();
        Debug.Log("camera=" + cameraT);
        controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeObject.Fade)
        {
            //スティックの傾きを取得
            Vector2 stickSlope = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);

            //HMDの向きを取得し，縦方向の要素を0にする
            Vector3 forward = duration.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = duration.right;
            right.y = 0;
            right.Normalize();

            //HMDの向きを前方として，スティックの傾き方向に移動
            //壁との衝突のためRigidbodyのMovePositionを使用
            //壁以外との衝突はしてほしくないのでLayer Collision MatrixでCameraRigレイヤーとWallレイヤーを作成しています。
            cameraRb.MovePosition(cameraT.position + moveSpeed * (stickSlope.y * forward + stickSlope.x * right));
        }
    }
}
