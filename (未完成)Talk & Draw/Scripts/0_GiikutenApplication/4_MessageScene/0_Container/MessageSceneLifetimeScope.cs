using GiikutenApplication.Common;
using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.MessageScene.Domain.Usecase;
using GiikutenApplication.MessageScene.Presentation.IView;
using GiikutenApplication.MessageScene.View;
using GiikutenApplication.Transition;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.MessageScene.Container
{
    /// <summary>
    /// MessageSceneのDIコンテナ
    /// </summary>
    public class MessageSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("View")]
        [SerializeField] private BackToHomeButtonView _backToHomeButtonView;
        [SerializeField] private SelectUserView _selectUserView;
        [Header("Transition")]
        [SerializeField] private TransitionPanelToHome _transitionPanelToHome;


        protected override void Configure(IContainerBuilder builder)
        {
            //MonoBehaviour継承クラスの登録
            //View
            builder.RegisterInstance(_backToHomeButtonView).As<IBackToHomeButtonView>();
            builder.RegisterComponent(_selectUserView).As<ISelectUserView>();

            //Transition
            builder.RegisterComponent(_transitionPanelToHome);


            //pureC#クラスの登録
            //エントリポイント
            builder.RegisterEntryPoint<MessageSceneEntryPoint>(Lifetime.Singleton);

            //リポジトリ
            builder.Register<IMessageSceneRepository, TestMessageSceneRepository>(Lifetime.Singleton);

            //ユースケース
            builder.Register<MessageSceneLoadUsecase>(Lifetime.Singleton);
            builder.Register<BackToHomeSceneUsecase>(Lifetime.Singleton);

            //Model

            //Presenter
            builder.Register<BackToHomeButtonPresenter>(Lifetime.Singleton);

            //その他
            builder.Register<OnlineImageGetter>(Lifetime.Singleton);
        }
    }
}