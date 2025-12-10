using GiikutenApplication.HomeScene.Presentation.IView;
using System;
using UniRx;
using UnityEngine;
using UtilView;

namespace GiikutenApplication.HomeScene.View
{
    /// <summary>
    /// 画面下部のボタン群の入力を受け取るView
    /// </summary>
    public class MenuButtonView : MonoBehaviour, IMenuButtonView
    {
        //各UI要素
        [SerializeField] private ClickableButton _battleButton; //Battleボタン
        [SerializeField] private ClickableButton _packButton; //Packボタン
        [SerializeField] private ClickableButton _messageButton; //Messageボタン
        [SerializeField] private ClickableButton _forumButton; //Forumボタン
        [SerializeField] private ClickableButton _settingsButton; //Settingsボタン


        public IObservable<Unit> OnBattleButtonClick => _battleButton.OnClickSubject;

        public IObservable<Unit> OnPackButtonClick => _packButton.OnClickSubject;

        public IObservable<Unit> OnMessageButtonClick => _messageButton.OnClickSubject;

        public IObservable<Unit> OnForumButtonClick => _forumButton.OnClickSubject;

        public IObservable<Unit> OnSettingsButtonClick => _settingsButton.OnClickSubject;
    }
}