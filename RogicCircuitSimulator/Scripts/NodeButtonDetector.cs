using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// クリックされたとき、登録されているメソッドを呼び出す
/// </summary>
public class NodeButtonDetector : MonoBehaviour
{
    //通知先
    [SerializeField] private ButtonEvent _onButtonClick = new ButtonEvent();


    void OnMouseDown()
    {
        //ボタンが押されたら通知を送る
        _onButtonClick.Invoke();
    }


    /// <summary>
    /// ボタンイベント
    /// 通知を受け取ることができるメソッド
    /// </summary>
    [Serializable]
    public class ButtonEvent : UnityEvent
    {
    }
}
