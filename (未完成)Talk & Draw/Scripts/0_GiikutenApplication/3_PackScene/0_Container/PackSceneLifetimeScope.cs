using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.PackScene.Domain;
using GiikutenApplication.PackScene.Domain.Model;
using GiikutenApplication.PackScene.Domain.Usecase;
using GiikutenApplication.PackScene.Presentation.IView;
using GiikutenApplication.PackScene.Presentation.Presenter;
using GiikutenApplication.PackScene.View;
using GiikutenApplication.Pop;
using GiikutenApplication.Transition;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.PackScene.Container
{
    /// <summary>
    /// PackSceneのDIコンテナ
    /// </summary>
    public class PackSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("View")]
        [SerializeField] private BackToHomeButtonView _backToHomeButtonView;
        [SerializeField] private PackScreenView _packScreenView;
        [Header("Transition")]
        [SerializeField] private TransitionPanelToHome _transitionPanelToHome;
        [Header("Pop")]
        [SerializeField] private PopCanvas _cautionPop;
        [SerializeField] private PackOpenAnimationCanvas _packOpenAnimationCanvas;

        protected override void Configure(IContainerBuilder builder)
        {
            //MonoBehaviour継承クラスの登録
            //View
            builder.RegisterInstance(_backToHomeButtonView).As<IBackToHomeButtonView>();
            builder.RegisterInstance(_packScreenView).As<IPackScreenView>();

            //Transition
            builder.RegisterComponent(_transitionPanelToHome);

            //Pop
            builder.RegisterInstance(_cautionPop);
            builder.RegisterInstance(_packOpenAnimationCanvas);


            //pureC#クラスの登録
            //エントリポイント
            builder.RegisterEntryPoint<PackSceneEntryPoint>(Lifetime.Singleton);

            //リポジトリ
            builder.Register<IPackSceneRepository, TestPackSceneRepository>(Lifetime.Singleton);

            //ユースケース
            builder.Register<PackSceneLoadUsecase>(Lifetime.Singleton);
            builder.Register<PackOpenUsecase>(Lifetime.Singleton);
            builder.Register<BackToHomeSceneUsecase>(Lifetime.Singleton);

            //Model
            builder.Register<PackScreenModel>(Lifetime.Singleton);

            //Presenter
            builder.Register<BackToHomeButtonPresenter>(Lifetime.Singleton);
            builder.Register<PackScreenPresenter>(Lifetime.Singleton);
        }
    }
}