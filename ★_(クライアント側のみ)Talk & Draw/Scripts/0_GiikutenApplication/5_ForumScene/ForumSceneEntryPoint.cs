using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.ForumScene.Domain.Usecase;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.ForumScene
{
    /// <summary>
    /// ForumSceneのエントリポイント
    /// </summary>
    public class ForumSceneEntryPoint : IInitializable
    {
        //対象のPresenter,ユースケース
        [Inject] private readonly BackToHomeButtonPresenter _backToHomeButtonPresenter;
        [Inject] private readonly ForumSceneLoadUsecase _forumSceneLoadUsecase;


        void IInitializable.Initialize()
        {
            Debug.Log($"<color=green>ForumSceneの制御を開始します</color>");

            //Presenterの初期化処理
            _backToHomeButtonPresenter.Init();

            //シーン初期化ユースケースを実行
            _forumSceneLoadUsecase.Execute();
        }
    }
}