using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ドリンク棚のドアの制御クラス
/// </summary>
public class DrinkDoor : MonoBehaviour, IActionable
{
    //参照
    [SerializeField] private Animator _animator;

    //開閉状況
    private bool _isOpen = false;


    public void UserAction()
    {
        //閉じていたら開ける、開いていたら閉じる
        if (_isOpen == false)
        {
            _animator.SetTrigger($"Open");
            _isOpen = true;
        }
        else
        {
            _animator.SetTrigger($"Close");
            _isOpen = false;
        }
    }
}
