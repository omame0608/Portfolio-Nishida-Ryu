using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Nodeファクトリのインターフェース
/// </summary>
public interface INodeFactory
{
    //Nodeの種類一覧
    public enum NodeType
    {
        AND,
        OR,
        NOT,
        INPUT,
        OUTPUT
    }


    /// <summary>
    /// 指定したNodeを生成する
    /// </summary>
    /// <param name="nodeType">生成するNodeを指定</param>
    /// <param name="position">生成する座標</param>
    void Create(NodeType nodeType, Vector3 position);
}
