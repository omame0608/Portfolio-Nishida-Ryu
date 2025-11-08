using System;
using UniRx;

namespace PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps
{
    /// <summary>
    /// 「金のネジ」取得を発行するインターフェース
    /// </summary>
    public interface IGoldenScrewGetNotifier
    {
        /// <summary>
        /// 「金のネジ」を取得した際に発火
        /// </summary>
        IObservable<Unit> OnGoldenScrewGetDetected { get; }
    }
}