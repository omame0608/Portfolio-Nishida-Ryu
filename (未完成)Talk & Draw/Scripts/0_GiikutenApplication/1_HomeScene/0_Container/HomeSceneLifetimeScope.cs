using GiikutenApplication.HomeScene.Domain;
using GiikutenApplication.HomeScene.Domain.Model;
using GiikutenApplication.HomeScene.Domain.Usecase;
using GiikutenApplication.HomeScene.Presentation;
using GiikutenApplication.HomeScene.Presentation.IView;
using GiikutenApplication.HomeScene.Presentation.Presenter;
using GiikutenApplication.HomeScene.View;
using GiikutenApplication.Transition;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.HomeScene.Container
{
    /// <summary>
    /// HomeSceneのDIコンテナ
    /// </summary>
    public class HomeSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("View")]
        [SerializeField] private UserHeaderView _userHeaderView;
        [SerializeField] private MenuButtonView _menuButtonView;
        [SerializeField] private BackGroundView _backGroundView;
        [Header("Transition")]
        [SerializeField] private TransitionPanel _transitionPanelToSettings;
        [SerializeField] private TransitionPanel _transitionPanelToForum;
        [SerializeField] private TransitionPanel _transitionPanelToMessage;
        [SerializeField] private TransitionPanel _transitionPanelToPack;
        [SerializeField] private TransitionPanel _transitionPanelToBattle;


        protected override void Configure(IContainerBuilder builder)
        {
            //MonoBehaviour継承クラスの登録
            //View
            builder.RegisterInstance(_userHeaderView).As<IUserHeaderView>();
            builder.RegisterInstance(_menuButtonView).As<IMenuButtonView>();
            builder.RegisterInstance(_backGroundView).As<IBackGroundView>();

            //Transition
            builder.RegisterInstance(_transitionPanelToSettings).Keyed("Settings");
            builder.RegisterInstance(_transitionPanelToForum).Keyed("Forum");
            builder.RegisterInstance(_transitionPanelToMessage).Keyed("Message");
            builder.RegisterInstance(_transitionPanelToPack).Keyed("Pack");
            builder.RegisterInstance(_transitionPanelToBattle).Keyed("Battle");


            //pureC#クラスの登録
            //エントリポイント
            builder.RegisterEntryPoint<HomeSceneEntryPoint>(Lifetime.Singleton);

            //リポジトリ
            builder.Register<IHomeSceneRepository, TestHomeSceneRepository>(Lifetime.Singleton);

            //ユースケース
            builder.Register<HomeSceneLoadUsecase>(Lifetime.Singleton);
            builder.Register<BattleButtonClickUsecase>(Lifetime.Singleton);
            builder.Register<PackButtonClickUsecase>(Lifetime.Singleton);
            builder.Register<MessageButtonClickUsecase>(Lifetime.Singleton);
            builder.Register<ForumButtonClickUsecase>(Lifetime.Singleton);
            builder.Register<SettingsButtonClickUsecase>(Lifetime.Singleton);
            builder.Register<BackGroundChangeUsecase>(Lifetime.Singleton);

            //Model
            builder.Register<UserHeaderModel>(Lifetime.Singleton);

            //Presenter
            builder.Register<UserHeaderPresenter>(Lifetime.Singleton);
            builder.Register<MenuButtonPresenter>(Lifetime.Singleton);
        }
    }
}