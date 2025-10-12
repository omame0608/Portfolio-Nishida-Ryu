using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour
{
    private OVRInput.Controller controller;

    private IDisplayable text;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<OVRControllerHelper>().m_controller;
        text = GameObject.Find("Text (Legacy)").GetComponent<IDisplayable>();
        if (GameObject.Find("Text (Legacy)") == null) { Debug.Log("owaridesu"); }
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One,controller))
        {
            text.Display("ボタン押されてますよ");
        }
        else
        {
            text.Display("ボタン押されてませんよ");
        }
    }
}
