using GiikutenApplication.HomeScene.Domain.Usecase;
using GiikutenApplication.HomeScene.Presentation.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.HomeScene
{
    /// <summary>
    /// HomeSceneのエントリポイント
    /// </summary>
    public class HomeSceneEntryPoint : IInitializable
    {
        //対象のPresenter,ユースケース
        [Inject] private readonly UserHeaderPresenter _userHeaderPresenter;
        [Inject] private readonly MenuButtonPresenter _menuButtonPresenter;
        [Inject] private readonly HomeSceneLoadUsecase _homeSceneLoadUsecase;
        [Inject] private readonly BackGroundChangeUsecase _backGroundChangeUsecase;


        async void IInitializable.Initialize()
        {
            Debug.Log($"<color=green>HomeSceneの制御を開始します</color>");

            //Presenterの初期化処理
            _userHeaderPresenter.Init();
            _menuButtonPresenter.Init();

            //シーン初期化ユースケースを実行
            await _homeSceneLoadUsecase.Execute();
            _backGroundChangeUsecase.Execute();
        }
    }
}
