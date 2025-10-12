using ParkingGame.Field;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems.EndCondition
{
    /// <summary>
    /// ゴール枠に3秒滞在したらクリア
    /// </summary>
    public class GoalRule : IParkingGameFinishable
    {
        //監視対象
        //動的に登録する
        public GoalDetector GoalDetector;


        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            GoalDetector?.OnGoal.Subscribe(_ => finishedCallback(GameResultType.Win));
        }
    }
}