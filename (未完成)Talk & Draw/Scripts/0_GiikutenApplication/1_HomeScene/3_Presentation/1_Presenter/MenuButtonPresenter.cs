using GiikutenApplication.HomeScene.Domain.Usecase;
using GiikutenApplication.HomeScene.Presentation.IView;
using System;
using UniRx;
using VContainer;

namespace GiikutenApplication.HomeScene.Presentation.Presenter
{
    /// <summary>
    /// 画面下部のボタン群のPresenter
    /// </summary>
    public class MenuButtonPresenter : IDisposable
    {
        //対象のModel,View,Usecase
        [Inject] private readonly IMenuButtonView _menuButtonView;
        [Inject] private readonly BattleButtonClickUsecase _battleButtonClickUsecase;
        [Inject] private readonly PackButtonClickUsecase _packButtonClickUsecase;
        [Inject] private readonly MessageButtonClickUsecase _messageButtonClickUsecase;
        [Inject] private readonly ForumButtonClickUsecase _forumButtonClickUsecase;
        [Inject] private readonly SettingsButtonClickUsecase _settingsButtonClickUsecase;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        /// <summary>
        /// イベント購読処理
        /// </summary>
        public void Init()
        {
            _menuButtonView.OnBattleButtonClick
                .Subscribe(_ => _battleButtonClickUsecase.Execute())
                .AddTo(_disposables);

            _menuButtonView.OnPackButtonClick
                .Subscribe(_ => _packButtonClickUsecase.Execute())
                .AddTo(_disposables);

            _menuButtonView.OnMessageButtonClick
                .Subscribe(_ => _messageButtonClickUsecase.Execute())
                .AddTo(_disposables);

            _menuButtonView.OnForumButtonClick
                .Subscribe(_ => _forumButtonClickUsecase.Execute())
                .AddTo(_disposables);

            _menuButtonView.OnSettingsButtonClick
                .Subscribe(_ => _settingsButtonClickUsecase.Execute())
                .AddTo(_disposables);
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}
