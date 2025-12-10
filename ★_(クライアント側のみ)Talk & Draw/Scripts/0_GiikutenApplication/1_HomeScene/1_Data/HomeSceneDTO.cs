using System;
using UnityEngine;

namespace GiikutenApplication.HomeScene.Data
{
    /// <summary>
    /// HomeScene構築用のDTO
    /// </summary>
    [Serializable]
    public class HomeSceneDTO
    {
        //HomeScene構築に利用できるデータ一覧
        [SerializeField]private string _userName; //ユーザの名前
        [SerializeField]private string _jobName; //ユーザのジョブ名
        [SerializeField]private int _gachaStoneAmount; //ガチャ石の所持数


        public HomeSceneDTO(){}
        public HomeSceneDTO(string userName, string jobName, int gachaStoneAmount)
        {
            _userName = userName;
            _jobName = jobName;
            _gachaStoneAmount = gachaStoneAmount;
        }


        //プロパティ一覧
        public string UserName => _userName;
        public string JobName => _jobName;
        public int GachaStoneAmount => _gachaStoneAmount;
    }
}
