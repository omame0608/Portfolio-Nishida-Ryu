using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.PackScene.Domain.Usecase;
using GiikutenApplication.PackScene.Presentation.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.PackScene
{
    /// <summary>
    /// PackSceneのエントリポイント
    /// </summary>
    public class PackSceneEntryPoint : IInitializable
    {
        //対象のPresenter,ユースケース
        [Inject] private readonly BackToHomeButtonPresenter _backToHomeButtonPresenter;
        [Inject] private readonly PackScreenPresenter _packScreenPresenter;
        [Inject] private readonly PackSceneLoadUsecase _packSceneLoadUsecase;


        void IInitializable.Initialize()
        {
            Debug.Log($"<color=green>PackSceneの制御を開始します</color>");

            //Presenterの初期化処理
            _backToHomeButtonPresenter.Init();
            _packScreenPresenter.Init();

            //シーン初期化ユースケースを実行
            _packSceneLoadUsecase.Execute();
        }
    }
}