using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems.EndCondition
{
    /// <summary>
    /// ゲーム終了条件を管理するクラス
    /// </summary>
    public class ParkingGameFinishableCollection
    {
        //ゲーム終了条件リスト
        [Inject] private readonly IEnumerable<IParkingGameFinishable> _finishList;


        /// <summary>
        /// ゲーム終了条件の監視を適用する
        /// </summary>
        /// <param name="finishedCallback">終了時のコールバック関数</param>
        public void ApplyFinishRule(Action<GameResultType> finishedCallback)
        {
            foreach (var finishable in _finishList)
            {
                finishable.StartObserve(finishedCallback);
            }
        }
    }
}
