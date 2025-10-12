using ParkingGame.GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Test
{
    /// <summary>
    /// モッククラス：shiftキーを押したらステージ終了
    /// </summary>
    public class MocShiftClickRule : IParkingGameFinishable
    {
        //監視対象
        [Inject] private readonly CheckShift _checkShift;

        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            _checkShift.OnShiftClicked += () => finishedCallback(GameResultType.Lose);
        }
    }
}