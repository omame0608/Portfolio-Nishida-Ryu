using System;
using System.Collections.Generic;
using VContainer;

namespace SupernovaTrolley.GameCores.EndConditions
{
    /// <summary>
    /// ゲーム終了条件を管理
    /// </summary>
    public class GameFinishableCollection
    {
        //ゲーム終了条件のリスト
        [Inject] private readonly IEnumerable<IGameFinishable> _finishList;


        /// <summary>
        /// ゲーム終了条件を適用
        /// </summary>
        /// <param name="gameFinishCallback"></param>
        public void ApplyFinishRule(Action<GameResultType> gameFinishCallback)
        {
            foreach (var finishable in _finishList)
            {
                finishable.StartObserve(gameFinishCallback);
            }
        }


        /// <summary>
        /// ゲーム終了条件を解除
        /// </summary>
        public void RemoveFinishRule()
        {
            foreach (var finishable in _finishList)
            {
                finishable.StopObserve();
            }
        }
    }
}