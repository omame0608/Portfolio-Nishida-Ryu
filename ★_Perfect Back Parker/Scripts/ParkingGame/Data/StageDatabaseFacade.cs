using ParkingGame.GameSystems.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.Data
{
    /// <summary>
    /// ステージ情報DBMSクラス
    /// </summary>
    public class StageDatabaseFacade : IStageDatabaseFacade
    {
        //DB
        [Inject] private StageDataAsset _database;

        
        public StageData GetInfoWithStageNumber(int num)
        {
            //ステージ番号からIDを計算
            int id = num - 1;

            //DBからステージ情報を取得し返す
            return _database.StageDataList[id];
        }
    }
}