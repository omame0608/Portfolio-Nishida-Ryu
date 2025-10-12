using System;
using UniRx;

namespace GiikutenApplication.PackScene.Presentation.IView
{
    /// <summary>
    /// Pack画面で利用するデータViewのインターフェース
    /// </summary>
    public interface IPackScreenView
    {
        /// <summary>
        /// ガチャ石の所持数を更新
        /// </summary>
        /// <param name="amount">更新後の値</param>
        void UpdateGachaStoneAmount(int amount);

        /// <summary>
        /// ランダムパックを引くとき発火
        /// </summary>
        IObservable<Unit> OnClickRandomPack { get; }

        /// <summary>
        /// レコメンドパックを引くとき発火
        /// </summary>
        IObservable<Unit> OnClickRecommendPack { get; }
    }
}