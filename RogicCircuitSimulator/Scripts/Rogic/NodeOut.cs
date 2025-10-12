using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// 仮想アウトプットノードを表現するクラス
/// </summary>
public class NodeOut : MonoBehaviour
{
    //データ本体
    private bool _data;

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;

    //監視しているInputPort
    [SerializeField] private InputPort _inputPort;


    void Start()
    {
        //inputPortのDataを監視する
        _inputPort.Data.Subscribe(data =>
        {
            //データ受け取って更新する
            _data = data;

            //カラー変更パーツを更新
            if (_data)
            {
                _colorParts.color = Color.red;
            }
            else
            {
                _colorParts.color = Color.black;
            }
        }).AddTo(this);
    }
}
