using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Connectionファクトリのインターフェース
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Connectionを生成する
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    void Create(InputPort inputPort, OutputPort outputPort);


    /// <summary>
    /// 繋いだポート間を繋ぐか削除するかを判定
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    /// <returns>繋ぐならtrue、削除ならfalse</returns>
    bool JudgeConnectOrDispose(InputPort inputPort, OutputPort outputPort);


    /// <summary>
    /// 接続済みのConnectionを削除する
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    void Dispose(InputPort inputPort, OutputPort outputPort);
}
