using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.SettingsScene.Domain.Usecase;
using GiikutenApplication.SettingsScene.Presentation.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.SettingsScene
{
    /// <summary>
    /// SettingsSceneのエントリポイント
    /// </summary>
    public class SettingsSceneEntryPoint : IInitializable
    {
        //対象のPresenter,ユースケース
        [Inject] private readonly BackToHomeButtonPresenter _backToHomeButtonPresenter;
        [Inject] private readonly InformationPresenter _informationPresenter;
        [Inject] private readonly SettingsSceneLoadUsecase _settingsSceneLoadUsecase;


        void IInitializable.Initialize()
        {
            Debug.Log($"<color=green>SettingsSceneの制御を開始します</color>");

            //Presenterの初期化処理
            _backToHomeButtonPresenter.Init();
            _informationPresenter.Init();

            //シーン初期化ユースケースを実行
            _settingsSceneLoadUsecase.Execute();
        }
    }
}