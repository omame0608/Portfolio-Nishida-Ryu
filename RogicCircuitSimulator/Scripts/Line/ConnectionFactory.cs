using System;
using UnityEditor;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

/// <summary>
/// Connectionのファクトリ
/// </summary>
public class ConnectionFactory : IConnectionFactory
{
    //参照
    [Inject] private Connection _connectionPrefab;
    //[Inject] private IObjectResolver _resolver;


    /// <summary>
    /// Connectionを生成する
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    public void Create(InputPort inputPort, OutputPort outputPort)
    {
        //生成
        Connection connection = Object.Instantiate(_connectionPrefab, Vector3.zero, Quaternion.identity);

        //初期設定
        //_resolver.Inject(connection);
        //inputPortにあたる座標を作成
        var inputPos = inputPort.transform.position;
        inputPos.z = -2f;

        //outputPortにあたる座標を作成
        var outputPos = outputPort.transform.position;
        outputPos.z = -2f;

        //connectionの見た目を設定
        var positions = new Vector3[] { inputPos, outputPos };
        connection.GetComponent<LineRenderer>().SetPositions(positions);

        //繋いだ両Portの依存関係を解決
        inputPort.InputConnection.Value = connection;
        inputPort.Init();
        outputPort.OutputConnections.Add(connection);
        outputPort.Init();
    }


    /// <summary>
    /// 繋いだポート間を繋ぐか削除するかを判定
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    /// <returns>繋ぐならtrue、削除ならfalse</returns>
    public bool JudgeConnectOrDispose(InputPort inputPort, OutputPort outputPort)
    {
        //Connectionの両側Portに登録されているConnection
        var outputList = outputPort.OutputConnections; //output側のリスト
        var input = inputPort.InputConnection.Value; //input側

        //input側が未接続であればtrue
        if (input == null) return true;

        //input側が接続済みの場合、その相手がoutput側になければtrue
        foreach(var output in outputList)
        {
            if (output == input) return false;
        }
        return true;
    }


    /// <summary>
    /// 接続済みのConnectionを削除する
    /// </summary>
    /// <param name="inputPort">入力ポート</param>
    /// <param name="outputPort">出力ポート</param>
    public void Dispose(InputPort inputPort, OutputPort outputPort)
    {
        //削除対象のconnectionを取得
        var connection = inputPort.InputConnection.Value;

        //inputPortとoutputPortの購読を解除する
        outputPort.OutputConnections.Remove(connection);
        inputPort.Dispose();

        //inputPortをリセット
        inputPort.Reset();

        //connectionを削除
        Object.Destroy(connection.gameObject);
    }
}
