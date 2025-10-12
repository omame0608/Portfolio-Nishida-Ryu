using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// ステージ失敗時のViewインターフェース
    /// </summary>
    public interface ILoseResultView
    {
        /// <summary>
        /// ステージ失敗UIを表示する
        /// </summary>
        void ShowLoseResult();


        /// <summary>
        /// ステージ失敗UIを非表示にする
        /// </summary>
        void HideLoseResult();
    }
}