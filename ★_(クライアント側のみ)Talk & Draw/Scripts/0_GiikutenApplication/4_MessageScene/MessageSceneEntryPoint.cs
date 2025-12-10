using Cysharp.Threading.Tasks;
using GiikutenApplication.Common.BackToHomeButton;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.MessageScene.Domain.Usecase;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.MessageScene
{
    /// <summary>
    /// MessageSceneのエントリポイント
    /// </summary>
    public class MessageSceneEntryPoint : IInitializable
    {
        //対象のPresenter,ユースケース
        [Inject] private readonly BackToHomeButtonPresenter _backToHomeButtonPresenter;
        [Inject] private readonly MessageSceneLoadUsecase _messageSceneLoadUsecase;

        //test
        [Inject] IMessageSceneRepository _messageSceneRepository;


        void IInitializable.Initialize()
        {
            Debug.Log($"<color=green>MessageSceneの制御を開始します</color>");

            //Presenterの初期化処理
            _backToHomeButtonPresenter.Init();

            //シーン初期化ユースケースを実行
            _messageSceneLoadUsecase.Execute();

            //test
            //Test();
        }


        async void Test()
        {
            var test = await _messageSceneRepository.FetchChatInfo();
            for (int i = 0; i < test.Length; i++)
            {
                Debug.Log($"{test[i].RoomID}, {test[i].UserName}, {test[i].ImageURL}");
            }
        }

    }
}