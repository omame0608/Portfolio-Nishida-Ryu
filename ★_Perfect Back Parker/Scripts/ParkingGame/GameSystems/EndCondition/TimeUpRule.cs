using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems.EndCondition
{
    /// <summary>
    /// 制限時間が来たらステージ失敗
    /// </summary>
    public class TimeUpRule : IParkingGameFinishable
    {
        //監視対象
        [Inject] private readonly StageTimer _stageTimer;


        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            _stageTimer.OnTimeUp.Subscribe(_ => finishedCallback(GameResultType.Lose));
        }
    }
}