using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// ノードの出力ポートを表現するクラス
/// </summary>
public class OutputPort : MonoBehaviour
{
    //データ本体
    private bool _data;

    //カラー変更パーツ
    [SerializeField] private SpriteRenderer _colorParts;

    //監視しているNode
    [SerializeField] private GameObject _myNodeObj;
    private IDataObservable _myNode;

    //出力先Connectionリスト
    public List<Connection> OutputConnections = new List<Connection>();


    void Start()
    {
        //NodeのDataを監視する
        _myNode = _myNodeObj.GetComponent<IDataObservable>();
        _myNode.Data.Subscribe(data =>
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

            Init();
        }).AddTo(this);
    }


    /// <summary>
    /// 明示的な更新を行う
    /// </summary>
    public void Init()
    {
        //Connectionリストにデータを伝播させる
        OutputConnections.ForEach(c => c.NotifyData(_data));
    }
}
