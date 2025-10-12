using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.Data
{
    /// <summary>
    /// ステージ情報リストを保持するスクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu(fileName = "StageData",
                     menuName = "ScriptableObjects/CreateStageDataAsset")]
    public class StageDataAsset : ScriptableObject
    {
        //ステージ情報リスト
        [SerializeField]private  List<StageData> _stageDataList = new List<StageData>();

        public List<StageData> StageDataList { get => _stageDataList; }
    }


    /// <summary>
    /// ステージ情報
    /// </summary>
    [System.Serializable]
    public class StageData
    {
        //ステージ情報
        [SerializeField] private string _stageName;       //ステージ名
        [SerializeField] private int _timeLimit;          //制限時間(秒)
        [SerializeField] private GameObject _stagePrefab; //ステージプレハブ

        public string StageName { get => _stageName; }
        public int TimeLimit { get => _timeLimit; }
        public GameObject StagePrefab { get => _stagePrefab; }
    }
}