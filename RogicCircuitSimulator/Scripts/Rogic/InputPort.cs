using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UniRx;
using UnityEngine;

/// <summary>
/// ノードの入力ポートを表現するクラス
/// </summary>
public class InputPort : MonoBehaviour, IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; } = new ReactiveProperty<bool>(false);

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;

    //入力元Connection

    public ReactiveProperty<Connection> InputConnection = new ReactiveProperty<Connection>(null);

    //購読解除用
    private IDisposable _dispose;


    /// <summary>
    /// 明示的な更新を行う
    /// </summary>
    public void Init()
    {
        //NodeのDataを監視する
        _dispose = InputConnection.Value.Data.Subscribe(data =>
        {
            //データ受け取って更新する
            Data.Value = data;

            ChangeColor();
            
        }).AddTo(this);
    }


    /// <summary>
    /// 購読解除用
    /// </summary>
    public void Dispose()
    {
        _dispose?.Dispose();
        _dispose = null;
        InputConnection.Value = null;
    }


    /// <summary>
    /// リセット用
    /// </summary>
    public void Reset()
    {
        Data.Value = false;
        ChangeColor();
    }


    /// <summary>
    /// カラー変更パーツを更新する
    /// </summary>
    private void ChangeColor()
    {
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
