using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// ゲームの終了条件を判定できるインターフェース
    /// </summary>
    public interface IParkingGameFinishable
    {
        /// <summary>
        /// ゲーム終了条件を監視する
        /// </summary>
        /// <param name="finishedCallback">終了判断時のコールバック関数</param>
        void StartObserve(Action<GameResultType> finishedCallback);
    }


    /// <summary>
    /// ステージの終了条件判定の結果
    /// </summary>
    public enum GameResultType
    {
        None, //null
        Win,  //ステージクリア
        Lose  //ステージ失敗
    }
}
