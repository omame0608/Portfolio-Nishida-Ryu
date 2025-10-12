using System;
using UniRx;

namespace GiikutenApplication.HomeScene.Presentation.IView
{
    /// <summary>
    /// 画面下部のボタン群の入力を受け取るViewのインターフェース
    /// </summary>
    public interface IMenuButtonView
    {
        /// <summary>
        /// Battleボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnBattleButtonClick { get; }

        /// <summary>
        /// Packボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnPackButtonClick { get; }

        /// <summary>
        /// Messageボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnMessageButtonClick { get; }

        /// <summary>
        /// Forumボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnForumButtonClick { get; }

        /// <summary>
        /// Settingsボタン押下時に発火する
        /// </summary>
        IObservable<Unit> OnSettingsButtonClick { get; }
    }
}
