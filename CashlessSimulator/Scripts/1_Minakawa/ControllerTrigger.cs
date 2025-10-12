using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//トリガー押下によってオブジェクトを保持する
//壁の外に行ったときの処理も追加予定
public class ControllerTrigger : MonoBehaviour
{
    private OVRInput.Controller controller;
    private bool isTriggerDown = false;
    private bool isActionCalled = false;
    //保持しているオブジェクト
    private IGrabbable grabbingObj=null;

    [SerializeField] private bool _isLeftHand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<OVRControllerHelper>().m_controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLeftHand)
        {
            if (grabbingObj == null) GameManager.Instance.LeftItem = GameManager.Item.None;
            else if (!((CrabbableObject)grabbingObj).IsDrink) GameManager.Instance.LeftItem = GameManager.Item.Food;
            else if (((CrabbableObject)grabbingObj).IsDrink) GameManager.Instance.LeftItem = GameManager.Item.Drink;
        }
        else
        {
            if (grabbingObj == null) GameManager.Instance.RightItem = GameManager.Item.None;
            else if (!((CrabbableObject)grabbingObj).IsDrink) GameManager.Instance.RightItem = GameManager.Item.Food;
            else if (((CrabbableObject)grabbingObj).IsDrink) GameManager.Instance.RightItem = GameManager.Item.Drink;
        }


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller)){
            isTriggerDown = true;
        }
        else
        {
            isTriggerDown=false;
            isActionCalled = false;

            if (grabbingObj == null) return;
            grabbingObj.OnRelease();
            grabbingObj = null;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (isTriggerDown & !isActionCalled)
        {
            IActionable selectObj= collider.gameObject.GetComponent<IActionable>();
            if (selectObj == null) return;
            selectObj.UserAction();
            isActionCalled = true;
            IGrabbable grabbableObj = collider.gameObject.GetComponent<IGrabbable>();
            if (grabbableObj == null) return;
            if (grabbableObj.OnGrab(this.gameObject.transform)) grabbingObj = grabbableObj;
            
        }
    }
}
