using Cysharp.Threading.Tasks;
using GiikutenApplication.Transition;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using Object = UnityEngine.Object;

namespace GiikutenApplication.HomeScene.Domain.Usecase
{
    /// <summary>
    /// Messageボタン押下時に行うシーン遷移処理
    /// </summary>
    public class MessageButtonClickUsecase
    {
        //対象のリポジトリ,Model
        [Inject, Key("Message")] private readonly TransitionPanel _transition;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:Messageボタン押下時に行うシーン遷移処理");

            Object.Instantiate(_transition);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            SceneManager.LoadScene("MessageScene");
        }
    }
}
