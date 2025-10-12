using ParkingGame.Data;
using ParkingGame.GameSystems.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：ステージ情報DBMSモッククラス
    /// </summary>
    public class MocStageDatabaseFacade : IStageDatabaseFacade
    {
        public StageData GetInfoWithStageNumber(int num)
        {
            Debug.Log($"<color=yellow>ステージ{num}の情報を返します</color>");
            return new StageData();
        }
    }
}