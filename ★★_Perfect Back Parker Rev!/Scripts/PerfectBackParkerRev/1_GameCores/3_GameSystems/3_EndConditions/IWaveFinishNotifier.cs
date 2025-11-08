using System;
using UniRx;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// ウェーブ終了通知を発行するインターフェース
    /// </summary>
    public interface IWaveFinishNotifier
    {
        /// <summary>
        /// ウェーブクリアまたは失敗を検出した際に発火
        /// </summary>
        IObservable<Unit> OnWaveFinishDetected { get; }
    }
}
