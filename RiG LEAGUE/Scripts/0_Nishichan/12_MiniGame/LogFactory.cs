using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 丸太生成用クラス
/// </summary>
/// <author>西田琉</author>
public class LogFactory : MonoBehaviour
{
    //プロダクト
    [SerializeField] private GameObject _logPrefab;

    //座標情報
    private static readonly Vector3 _LEFT_POSITION = new Vector3(-5.25f, -1.655f, 20f); //一番左の座標
    private static readonly Vector3 _DELTA_POSITION = new Vector3(1.5f, 0f, 0f);        //左のトロッコからの差分


    /// <summary>
    /// 丸太を三つ生成する
    /// </summary>
    /// <param name="id">流すレーン（プレイヤー）のID</param>
    public void InstantiateLogs(int id)
    {
        //スタート位置をずらす値
        var s = new Vector3(0f, 0f, UnityEngine.Random.Range(0f, 10f));
        //var s = new Vector3(0f, 0f, 10f); //最速
        //var s = new Vector3(0f, 0f, 0f); //最遅

        for (int i = 0; i < 3; i++)
        {
            //間隔をずらす値
            var d = new Vector3(0f, 0f, UnityEngine.Random.Range(13f, 15f));
            //var d = new Vector3(0f, 0f, 13f); //最速
            //var d = new Vector3(0f, 0f, 15f); //最遅

            //生成
            Instantiate(_logPrefab, _LEFT_POSITION + id * _DELTA_POSITION - s + i * d, _logPrefab.transform.rotation);
        }
    }
}
