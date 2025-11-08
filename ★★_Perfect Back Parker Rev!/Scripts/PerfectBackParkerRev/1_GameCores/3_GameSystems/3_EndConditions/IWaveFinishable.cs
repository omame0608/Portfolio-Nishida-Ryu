using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// ウェーブ終了条件を判定できるインターフェース
    /// </summary>
    public interface IWaveFinishable
    {
        /// <summary>
        /// 実際に判定を行う個々の通知者を追加
        /// </summary>
        /// <param name="notifier">追加する通知者</param>
        void AddNotifier(IWaveFinishNotifier notifier);

        /// <summary>
        /// 実際に判定を行う個々の通知者を削除
        /// </summary>
        /// <param name="notifier">削除する通知者</param>
        //void RemoveNotifier(IWaveFinishNotifier notifier);
        
        /// <summary>
        /// ゲーム終了条件を監視する
        /// </summary>
        /// <param name="waveFinishCallback">終了判定時のコールバック関数</param>
        void StartObserve(Action<WaveResultType> waveFinishCallback);

        /// <summary>
        /// ゲーム終了条件の監視を終了する
        /// </summary>
        void StopObserve();
    }


    /// <summary>
    /// /ウェーブ終了時の結果タイプ
    /// </summary>
    public enum WaveResultType
    {
        Clear, //ウェーブクリア
        Failed, //ウェーブ失敗
        None
    }
}