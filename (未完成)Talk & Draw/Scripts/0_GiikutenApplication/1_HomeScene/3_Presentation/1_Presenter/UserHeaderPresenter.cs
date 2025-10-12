using GiikutenApplication.HomeScene.Domain.Model;
using VContainer;
using UniRx;
using UnityEngine;
using System;

namespace GiikutenApplication.HomeScene.Presentation.Presenter
{
    /// <summary>
    /// 画面上部のユーザ情報を表示するヘッダーのPresenter
    /// </summary>
    public class UserHeaderPresenter : IDisposable
    {
        //対象のModel,View,Usecase
        [Inject] private readonly UserHeaderModel _userHeaderModel;
        [Inject] private readonly IUserHeaderView _userHeaderView;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        /// <summary>
        /// イベント購読処理
        /// </summary>
        public void Init()
        {
            _userHeaderModel.UserName
                .Subscribe(text => _userHeaderView.UpdateUserName(text))
                .AddTo(_disposables);

            _userHeaderModel.JobName
                .Subscribe(text => _userHeaderView.UpdateJobName(text))
                .AddTo(_disposables);

            _userHeaderModel.GachaStoneAmount
                .Subscribe(amount => _userHeaderView.UpdateGachaStoneAmount(amount))
                .AddTo(_disposables);
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}
