using GiikutenApplication.PackScene.Domain.Model;
using GiikutenApplication.PackScene.Domain.Usecase;
using GiikutenApplication.PackScene.Presentation.IView;
using System;
using UniRx;
using VContainer;

namespace GiikutenApplication.PackScene.Presentation.Presenter
{
    /// <summary>
    /// Pack画面で利用するデータPresenter
    /// </summary>
    public class PackScreenPresenter : IDisposable
    {
        //対象のModel,View,Usecase
        [Inject] private readonly PackScreenModel _packScreenModel;
        [Inject] private readonly IPackScreenView _packScreenView;
        [Inject] private readonly PackOpenUsecase _packOpenUsecase;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        /// <summary>
        /// イベント購読処理
        /// </summary>
        public void Init()
        {
            //ガチャ石表示
            _packScreenModel.GachaStoneAmount
                .Subscribe(amount => _packScreenView.UpdateGachaStoneAmount(amount))
                .AddTo(_disposables);

            //ランダムパック
            _packScreenView.OnClickRandomPack
                .Subscribe(_ => _packOpenUsecase.Execute(true))
                .AddTo(_disposables);

            //レコメンドパック
            _packScreenView.OnClickRecommendPack
                .Subscribe(_ => _packOpenUsecase.Execute(false))
                .AddTo(_disposables);
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}