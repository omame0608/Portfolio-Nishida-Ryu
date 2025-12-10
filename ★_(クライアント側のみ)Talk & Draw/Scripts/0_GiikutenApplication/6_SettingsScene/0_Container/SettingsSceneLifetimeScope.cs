using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.SettingsScene.Domain;
using GiikutenApplication.SettingsScene.Domain.Model;
using GiikutenApplication.SettingsScene.Domain.Usecase;
using GiikutenApplication.SettingsScene.Presentation.IView;
using GiikutenApplication.SettingsScene.Presentation.Presenter;
using GiikutenApplication.SettingsScene.Repository;
using GiikutenApplication.SettingsScene.View;
using GiikutenApplication.Transition;
using Test;//差し替え
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.SettingsScene.Container
{
    /// <summary>
    /// SettingsSceneのDIコンテナ
    /// </summary>
    public class SettingsSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("View")]
        [SerializeField] private BackToHomeButtonView _backToHomeButtonView;
        [SerializeField] private InformationView _informationView;
        [Header("Transition")]
        [SerializeField] private TransitionPanelToHome _transitionPanelToHome;


        protected override void Configure(IContainerBuilder builder)
        {
            //MonoBehaviour継承クラスの登録
            //View
            builder.RegisterInstance(_backToHomeButtonView).As<IBackToHomeButtonView>();
            builder.RegisterInstance(_informationView).As<IInformationView>();

            //Transition
            builder.RegisterComponent(_transitionPanelToHome);


            //pureC#クラスの登録
            //エントリポイント
            builder.RegisterEntryPoint<SettingsSceneEntryPoint>(Lifetime.Singleton);

            //リポジトリ
            builder.Register<ISettingsSneneRepository, SettingsSceneRepository>(Lifetime.Singleton);

            //ユースケース
            builder.Register<SettingsSceneLoadUsecase>(Lifetime.Singleton);
            builder.Register<SaveInformationUsecase>(Lifetime.Singleton);
            builder.Register<BackToHomeSceneUsecase>(Lifetime.Singleton);

            //Model
            builder.Register<InformationModel>(Lifetime.Singleton);

            //Presenter
            builder.Register<BackToHomeButtonPresenter>(Lifetime.Singleton);
            builder.Register<InformationPresenter>(Lifetime.Singleton);
        }
    }
}