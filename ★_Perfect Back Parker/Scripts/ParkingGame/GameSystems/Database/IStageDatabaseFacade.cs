using ParkingGame.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.Database
{
    /// <summary>
    /// ステージ情報DBMSインターフェース
    /// </summary>
    public interface IStageDatabaseFacade
    {
        /// <summary>
        /// ステージ番号からデータを取得
        /// </summary>
        /// <param name="num">ステージ番号</param>
        /// <returns>ステージ情報</returns>
        StageData GetInfoWithStageNumber(int num);
    }
}