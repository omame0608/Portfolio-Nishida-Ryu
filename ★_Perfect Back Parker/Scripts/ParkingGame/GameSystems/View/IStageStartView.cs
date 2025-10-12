using Cysharp.Threading.Tasks;
using ParkingGame.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// ステージ開始時のViewを操作できるインターフェース
    /// </summary>
    public interface IStageStartView
    {
        /// <summary>
        /// ステージ開始時のViewを表示
        /// </summary>
        /// <param name="stageData">ステージ情報</param>
        /// <returns></returns>
        UniTask ShowStageInfomation(StageData stageData);
    }
}