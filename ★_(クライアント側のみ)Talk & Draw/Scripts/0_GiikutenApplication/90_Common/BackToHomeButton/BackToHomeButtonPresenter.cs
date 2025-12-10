using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.Common.BackToHomeButton
{
    /// <summary>
    /// PackSceneからHomeSceneへ戻るボタンのPresenter
    /// </summary>
    public class BackToHomeButtonPresenter : IDisposable
    {
        //対象のModel,View,Usecase
        [Inject] private readonly IBackToHomeButtonView _backToHomeButtonView;
        [Inject] private readonly BackToHomeSceneUsecase _backToHomeSceneUsecase;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        /// <summary>
        /// イベント購読処理
        /// </summary>
        public void Init()
        {
            _backToHomeButtonView.OnBackButtonClick
                .Subscribe(_ => _backToHomeSceneUsecase.Execute())
                .AddTo(_disposables);
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}