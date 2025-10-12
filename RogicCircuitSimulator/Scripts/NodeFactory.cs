using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

/// <summary>
/// Nodeのファクトリ
/// </summary>
public class NodeFactory : INodeFactory
{
    //参照
    [Inject] private NodeAND _nodeAND;
    [Inject] private NodeOR _nodeOR;
    [Inject] private NodeNOT _nodeNOT;
    [Inject] private NodeIn _nodeIn;
    [Inject] private NodeOut _nodeOut;
    [Inject] private IObjectResolver _resolver;


    /// <summary>
    /// 指定したNodeを生成する
    /// </summary>
    /// <param name="nodeType">生成するNodeを指定</param>
    /// <param name="position">生成する座標</param>
    void INodeFactory.Create(INodeFactory.NodeType nodeType, Vector3 position)
    {
        position.z = 0f;

        switch (nodeType)
        {
            case INodeFactory.NodeType.AND:
                //生成
                var and = Object.Instantiate(_nodeAND, position, Quaternion.identity);
                //初期設定
                _resolver.Inject(and.transform.GetChild(1).GetComponent<ConnectionLiner>());
                _resolver.Inject(and.transform.GetChild(2).GetComponent<ConnectionLiner>());
                _resolver.Inject(and.transform.GetChild(3).GetComponent<ConnectionLiner>());
                break;
            case INodeFactory.NodeType.OR:
                //生成
                var or = Object.Instantiate(_nodeOR, position, Quaternion.identity);
                //初期設定
                _resolver.Inject(or.transform.GetChild(1).GetComponent<ConnectionLiner>());
                _resolver.Inject(or.transform.GetChild(2).GetComponent<ConnectionLiner>());
                _resolver.Inject(or.transform.GetChild(3).GetComponent<ConnectionLiner>());
                break;
            case INodeFactory.NodeType.NOT:
                //生成
                var not = Object.Instantiate(_nodeNOT, position, Quaternion.identity);
                //初期設定
                _resolver.Inject(not.transform.GetChild(1).GetComponent<ConnectionLiner>());
                _resolver.Inject(not.transform.GetChild(2).GetComponent<ConnectionLiner>());
                break;
            case INodeFactory.NodeType.INPUT:
                //生成
                var input = Object.Instantiate(_nodeIn, position, Quaternion.identity);
                //初期設定
                _resolver.Inject(input.transform.GetChild(1).GetComponent<ConnectionLiner>());
                break;
            case INodeFactory.NodeType.OUTPUT:
                //生成
                var output = Object.Instantiate(_nodeOut, position, Quaternion.identity);
                //初期設定
                _resolver.Inject(output.transform.GetChild(1).GetComponent<ConnectionLiner>());
                break;
        }
    }
}
