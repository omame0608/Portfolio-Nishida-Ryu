using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// ステージの残り時間を示すタイマーインターフェース
    /// </summary>
    public interface IStageTimerView
    {
        /// <summary>
        /// 残り時間を更新する
        /// </summary>
        /// <param name="time">更新後の残り時間（秒）</param>
        void UpdateTimerView(int time);


        /// <summary>
        /// タイマーを表示する
        /// </summary>
        void ShowTimer();


        /// <summary>
        /// タイマーを非表示にする
        /// </summary>
        void HideTimer();
    }
}