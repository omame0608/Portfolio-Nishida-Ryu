using System;
using UniRx;

namespace GiikutenApplication.Common.BackToHomeButton
{
    /// <summary>
    /// PackSceneからHomeSceneへ戻るボタンViewのインターフェース
    /// </summary>
    public interface IBackToHomeButtonView
    {
        /// <summary>
        /// HomeSceneへ戻るボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnBackButtonClick { get; }
    }
}