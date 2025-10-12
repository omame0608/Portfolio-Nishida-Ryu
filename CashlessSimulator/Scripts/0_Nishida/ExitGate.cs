using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 出口の制御
/// </summary>
[RequireComponent(typeof(Collider))]
public class ExitGate : MonoBehaviour
{
    //出口のドア
    [SerializeField] private Transform _exitDoor;

    //座標情報
    private Vector3 _close; //閉まっている状態の座標
    private Vector3 _open; //開いている状態の座標
    private int _count = 10; //アニメーションにかけるフレーム数
    private bool _isOpen; //開閉状態


    void Start()
    {
        //座標を決定
        _close = _exitDoor.position;
        _open = _close + new Vector3(0f, 0f, -1.053f);
    }


    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーがクリア可能状態で侵入したらドアを開ける
        if (other.CompareTag("Player") && GameManager.Instance.CanClear && !_isOpen)
        {
            _isOpen = true;
            StartCoroutine(Open());
        }
    }


    /// <summary>
    /// ドアを開ける
    /// </summary>
    private IEnumerator Open()
    {
        _exitDoor.position = _close;
        float offset = -1f / _count;
        for(int i = 0; i <= _count; i++)
        {
            _exitDoor.position += new Vector3(0f, 0f, offset);
            yield return null;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        //プレイヤーがゲートから離れたらドアを閉じる
        if (other.CompareTag("Player") && _isOpen)
        {
            _isOpen = false;
            StartCoroutine(Close());
        }
    }


    /// <summary>
    /// ドアを閉じる
    /// </summary>
    private IEnumerator Close()
    {
        _exitDoor.position = _open;
        float offset = 1f / _count;
        for (int i = 0; i <= _count; i++)
        {
            _exitDoor.position += new Vector3(0f, 0f, offset);
            yield return null;
        }
    }
}
