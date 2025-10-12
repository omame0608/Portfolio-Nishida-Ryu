using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//左コントローラのスティックでは視点の向きを制御
public class RightControllerStick : MonoBehaviour
{
    private Transform cameraT;
    private OVRInput.Controller controller;
    [SerializeField, Range(0f, 1f)]
    private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cameraT = GameObject.Find("[BuildingBlock] Camera Rig").GetComponent<Transform>();
        controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeObject.Fade)
        {
            Vector2 stickSlope = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
            if (stickSlope.x == 0f) return;
            cameraT.Rotate(0, rotateSpeed * stickSlope.x, 0);
        }
    }
}