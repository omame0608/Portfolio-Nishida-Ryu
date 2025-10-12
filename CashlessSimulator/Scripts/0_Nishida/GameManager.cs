using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// シミュレーションを管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    //自身を公開
    public static GameManager Instance { get; private set; }

    //UI操作用
    [SerializeField] private TextChange _textChange;

    //手に持っている商品の種別
    public enum Item
    {
        Drink,
        Food,
        None
    }

    //左右の手に持っている商品
    public Item LeftItem { get; set; }
    public Item RightItem { get; set; }

    //クリアが可能かどうか
    public bool CanClear { get; private set; }


    void Awake()
    {
        //自身を公開
        Instance = this;
    }


    void Update()
    {
        //頭上に表示するテキストを更新
        //デフォルト
        _textChange.Display("<color=red>ドリンク</color>と<color=red>食べ物</color>を手に取ろう");

        //食べ物だけ持っている場合
        if (RightItem == Item.Food && LeftItem == Item.None
            || RightItem == Item.None && LeftItem == Item.Food
            || RightItem == Item.Food && LeftItem == Item.Food)
        {
            _textChange.Display("<color=red>ドリンク</color>も手に取ろう");
        }

        //ドリンクだけ持っている場合
        if (RightItem == Item.Drink && LeftItem == Item.None
            || RightItem == Item.None && LeftItem == Item.Drink
            || RightItem == Item.Drink && LeftItem == Item.Drink)
        {
            _textChange.Display("<color=red>食べ物</color>も手に取ろう");
        }

        //食べ物とドリンクの両方を持っている場合
        if (RightItem == Item.Food && LeftItem == Item.Drink || RightItem == Item.Drink && LeftItem == Item.Food)
        {
            _textChange.Display("<color=green>出口ゲートを抜けよう</color>");
            CanClear = true;
        }
        else
        {
            CanClear = false;
        }
    }
}
