using Cysharp.Threading.Tasks;
using GiikutenApplication.Transition;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using Object = UnityEngine.Object;

namespace GiikutenApplication.Common.BackToHomeButton
{
    /// <summary>
    /// 任意のシーンからHomeSceneに戻るシーン遷移処理
    /// </summary>
    public class BackToHomeSceneUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly TransitionPanelToHome _transition;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:任意のシーンからHomeSceneに戻るシーン遷移処理");

            Object.Instantiate(_transition);

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

            SceneManager.LoadScene("HomeScene");
        }
    }
}