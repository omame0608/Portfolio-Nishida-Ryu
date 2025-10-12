using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// NOTノードを表現するクラス
/// </summary>
public class NodeNOT : MonoBehaviour, IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; } = new ReactiveProperty<bool>(false);

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;

    //監視しているInputPort
    [SerializeField] private InputPort _inputPort;

    //inputの値
    private bool _input;

    //購読解除用
    IDisposable _dispose;


    void Start()
    {
        //inputPortを監視し、input側connectionに変化があったら
        _inputPort.InputConnection.Subscribe(_ =>
        {
            //既に登録がある場合削除する
            _dispose?.Dispose();
            
            //inputPortのデータ監視の登録をしなおす
            _dispose = _inputPort.Data.Subscribe(data =>
            {
                //データ受け取って更新する
                _input = data;

                //演算を行い更新する
                Calculate(_input);

                //カラー変更パーツを更新
                if (Data.Value)
                {
                    _colorParts.color = Color.red;
                }
                else
                {
                    _colorParts.color = Color.black;
                }
            }).AddTo(this);
        });
    }


    /// <summary>
    /// NOT演算を行う
    /// </summary>
    /// <param name="input">入力</param>
    private void Calculate(bool input)
    {
        //AND演算を行い保持する
        Data.Value = !input;
        if (_inputPort.InputConnection.Value == null) Data.Value = false;
    }
}
