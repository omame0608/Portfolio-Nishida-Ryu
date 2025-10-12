using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 出力ポートと入力ポートを繋げる配線を表現するクラス
/// </summary>
public class Connection : MonoBehaviour, IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; } = new ReactiveProperty<bool>(false);

    //見た目本体
    public LineRenderer LineBody { get; private set; }


    void Awake()
    {
        LineBody = GetComponent<LineRenderer>();
    }


    /// <summary>
    /// connectionの保持するデータを更新する
    /// </summary>
    /// <param name="data">更新後の値</param>
    public void NotifyData(bool data)
    {
        //データを受け取って更新する
        Data.Value = data;

        //色を更新する
        if (Data.Value)
        {
            LineBody.startColor = Color.red;
            LineBody.endColor = Color.red;
        }
        else
        {
            LineBody.startColor = Color.black;
            LineBody.endColor = Color.black;
        }
    }
}
