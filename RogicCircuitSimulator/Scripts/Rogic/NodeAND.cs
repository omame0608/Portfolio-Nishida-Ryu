using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UniRx;
using UnityEngine;

/// <summary>
/// ANDノードを表現するクラス
/// </summary>
public class NodeAND : MonoBehaviour, IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; } = new ReactiveProperty<bool>(false);

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;

    //監視しているInputPort
    [SerializeField] private InputPort _inputPort1;
    [SerializeField] private InputPort _inputPort2;

    //inputの値
    private bool _input1;
    private bool _input2;


    void Start()
    {
        //inputPortのDataを監視する
        _inputPort1.Data.Subscribe(data =>
        {
            //データ受け取って更新する
            _input1 = data;

            //演算を行い更新する
            Calculate(_input1, _input2);

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

        _inputPort2.Data.Subscribe(data =>
        {
            //データ受け取って更新する
            _input2 = data;

            //演算を行い更新する
            Calculate(_input1, _input2);

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
    }


    /// <summary>
    /// AND演算を行う
    /// </summary>
    /// <param name="input1">入力1</param>
    /// <param name="input2">入力2</param>
    private void Calculate(bool input1, bool input2)
    {
        //AND演算を行い保持する
        Data.Value = input1 && input2;
    }
}
