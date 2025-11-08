using System;
using System.Collections.Generic;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// ゲーム終了条件をまとめて管理するクラス
    /// </summary>
    public class WaveFinishableCollection
    {
        //ゲーム終了条件リスト
        [Inject] private readonly IEnumerable<IWaveFinishable> _finishRuleList;


        /// <summary>
        /// ゲーム終了条件の監視を適用する
        /// </summary>
        /// <param name="waveFinishCallback">終了判定時のコールバック関数</param>
        public void ApplyFinishRule(Action<WaveResultType> waveFinishCallback)
        {
            foreach (var finishable in _finishRuleList)
            {
                finishable.StartObserve(waveFinishCallback);
            }
        }


        /// <summary>
        /// ゲーム終了条件の監視を解除する
        /// </summary>
        public void RemoveFinishRule()
        {
            foreach (var finishable in _finishRuleList)
            {
                finishable.StopObserve();
            }
        }
    }
}