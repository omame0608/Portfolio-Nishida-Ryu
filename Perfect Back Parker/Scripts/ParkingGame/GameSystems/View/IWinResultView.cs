using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// ステージクリア時のViewインターフェース
    /// </summary>
    public interface IWinResultView
    {
        /// <summary>
        /// ステージクリアUIを表示する
        /// </summary>
        void ShowWinResult();


        /// <summary>
        /// ステージクリアUIを非表示にする
        /// </summary>
        /// <param name="isFinal">最終ステージかどうか</param>
        void HideWinResult(bool isFinal = false);
    }
}