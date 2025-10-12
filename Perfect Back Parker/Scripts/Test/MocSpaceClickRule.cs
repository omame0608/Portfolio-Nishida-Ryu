using ParkingGame.GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Test
{
    /// <summary>
    /// モッククラス：スペースキーを押したらステージ終了
    /// </summary>
    public class MocSpaceClickRule : IParkingGameFinishable
    {
        //監視対象
        [Inject] private readonly CheckSpace _checkSpace;

        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            _checkSpace.OnSpaceClicked += () => finishedCallback(GameResultType.Win);
        }
    }
}
