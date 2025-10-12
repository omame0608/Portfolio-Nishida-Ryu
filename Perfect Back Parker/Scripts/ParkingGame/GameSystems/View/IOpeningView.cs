using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems.View
{
    /// <summary>
    /// オープニング画面のView
    /// </summary>
    public interface IOpeningView
    {
        /// <summary>
        /// オープニングViewを始める
        /// </summary>
        void PlayOpeningView();


        /// <summary>
        /// オープニングViewを終了する
        /// </summary>
        UniTask FinishOpeningView();
    }
}