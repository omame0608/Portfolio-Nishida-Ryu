using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コイン生成用クラス
/// </summary>
/// <author>西田琉</author>
public class CoinFactory : MonoBehaviour
{
    //プロダクト
    [SerializeField] private GameObject _coinPrefab;

    //座標情報
    private static readonly Vector3 _LEFT_POSITION = new Vector3(-5.25f, 1.06f, 20f); //一番左の座標
    private static readonly Vector3 _DELTA_POSITION = new Vector3(1.5f, 0f, 0f);      //左のトロッコからの差分


    /// <summary>
    /// コインを三つ生成する
    /// </summary>
    /// <param name="id">流すレーン（プレイヤー）のID</param>
    public void InstantiateCoins(int id)
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
            Instantiate(_coinPrefab, _LEFT_POSITION + id * _DELTA_POSITION - s + i * d, _coinPrefab.transform.rotation);
        }
    }
}
