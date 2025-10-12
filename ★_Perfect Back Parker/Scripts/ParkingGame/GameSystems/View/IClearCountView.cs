using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// クリアまでのカウントを表示するViewインターフェース
    /// </summary>
    public interface IClearCountView
    {
        bool CanDisplayCircle { get; set; }

        /// <summary>
        /// カウントを1秒分だけ表示
        /// </summary>
        /// <param name="count">表示するカウント数</param>
        void ShowCountOnce(int count);


        /// <summary>
        /// カウントを瞬時に停止し非表示にする
        /// </summary>
        void Cancel();
    }
}