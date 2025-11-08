using System.Collections.Generic;
using UnityEngine;

namespace PerfectBackParkerRev.Databases
{
    /// <summary>
    /// ステージデータが格納されたアセット
    /// </summary>
    [CreateAssetMenu(fileName = "StageDataAsset",
                     menuName = "PerfectBackParkerRev/StageDataAsset")]
    public class StageDataAsset : ScriptableObject
    {
        //ステージ情報一覧
        [SerializeField] private List<StageData> _stageDataList = new();

        public List<StageData> StageDataList => _stageDataList;
    }


    /// <summary>
    /// 各ステージのデータ
    /// </summary>
    [System.Serializable]
    public class StageData
    {
        //ステージのデータ一覧
        [SerializeField] private string _stageName; //ステージ名
        [SerializeField] private string _overView; //ステージ概要
        [SerializeField] private string _sceneName; //シーン名
        [SerializeField] private List<WaveData> _waveDataList = new(); //ウェーブデータ一覧

        public string StageName => _stageName;
        public string OverView => _overView;
        public string SceneName => _sceneName;
        public List<WaveData> WaveDataList => _waveDataList;
    }


    /// <summary>
    /// 各ウェーブのデータ
    /// </summary>
    [System.Serializable]
    public class WaveData
    {
        //ウェーブのデータ一覧
        [SerializeField] private string _waveName; //ウェーブ名
        [SerializeField] private int _waveBaseScore; //ウェーブ基礎点
        [SerializeField] private int _timeLimit; //制限時間
        [SerializeField] private Vector3 _startPosition; //スタート位置
        [SerializeField] private bool _isStartWithZPlusDirection; //Zプラス方向向きからスタートするかどうか
        [SerializeField] private GameObject _wavePrefab; //ウェーブプレハブ

        public string WaveName => _waveName;
        public int WaveBaseScore => _waveBaseScore;
        public int TimeLimit => _timeLimit;
        public Vector3 StartPosition => _startPosition;
        public bool IsStartWithZPlusDirection => _isStartWithZPlusDirection;
        public GameObject WavePrefab => _wavePrefab;
    }
}