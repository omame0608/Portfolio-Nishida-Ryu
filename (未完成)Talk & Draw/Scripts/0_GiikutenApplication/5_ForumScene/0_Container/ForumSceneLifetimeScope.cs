using GiikutenApplication.Common;
using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.ForumScene.Domain;
using GiikutenApplication.ForumScene.Domain.Usecase;
using GiikutenApplication.ForumScene.Presentation.IView;
using GiikutenApplication.ForumScene.View;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.MessageScene.Presentation.IView;
using GiikutenApplication.MessageScene.View;
using GiikutenApplication.Transition;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.ForumScene.Container
{
    /// <summary>
    /// ForumSceneのDIコンテナ
    /// </summary>
    public class ForumSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("View")]
        [SerializeField] private BackToHomeButtonView _backToHomeButtonView;
        [SerializeField] private SelectTopicView _selectTopicView;
        [Header("Transition")]
        [SerializeField] private TransitionPanelToHome _transitionPanelToHome;


        protected override void Configure(IContainerBuilder builder)
        {
            //MonoBehaviour継承クラスの登録
            //View
            builder.RegisterInstance(_backToHomeButtonView).As<IBackToHomeButtonView>();
            builder.RegisterComponent(_selectTopicView).As<ISelectTopicView>();

            //Transition
            builder.RegisterComponent(_transitionPanelToHome);


            //pureC#クラスの登録
            //エントリポイント
            builder.RegisterEntryPoint<ForumSceneEntryPoint>(Lifetime.Singleton);

            //リポジトリ
            builder.Register<IForumSceneRepository, TestForumSceneRepository>(Lifetime.Singleton);

            //ユースケース
            builder.Register<ForumSceneLoadUsecase>(Lifetime.Singleton);
            builder.Register<BackToHomeSceneUsecase>(Lifetime.Singleton);

            //Model

            //Presenter
            builder.Register<BackToHomeButtonPresenter>(Lifetime.Singleton);

            //その他
            builder.Register<OnlineImageGetter>(Lifetime.Singleton);
        }
    }
}