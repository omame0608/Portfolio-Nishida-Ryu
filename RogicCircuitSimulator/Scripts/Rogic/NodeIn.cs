using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 仮想インプットノードを表現するクラス
/// </summary>
public class NodeIn : MonoBehaviour, IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; } = new ReactiveProperty<bool>(false);

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;


    /// <summary>
    /// スイッチが押されたときOnOffを切り替える
    /// </summary>
    public void OnButtonPush()
    {
        //データを切り替える
        Data.Value = !Data.Value;

        //カラー変更パーツを更新
        if (Data.Value)
        {
            _colorParts.color = Color.red;
        }
        else
        {
            _colorParts.color = Color.black;
        }
    }
}
