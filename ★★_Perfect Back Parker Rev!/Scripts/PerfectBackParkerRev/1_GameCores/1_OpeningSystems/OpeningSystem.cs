using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.OpeningSystems.Sequences;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameCores.OpeningSystems
{
    /// <summary>
    /// オープニングシステム
    /// </summary>
    public class OpeningSystem : IOpeningInputHandler
    {
        //システム
        [Inject] private readonly OpeningSequence _openingSequence;
        [Inject] private readonly SceneLoader _sceneLoader;

        //制御用
        private bool _canInteract = true; //入力に対して応答が可能か


        /// <summary>
        /// シーンロードを待機した後オープニングシステムを起動する
        /// </summary>
        public async void Initialize()
        {
            //シーンロード完了を待機
            await UniTask.WaitUntil(() => !_sceneLoader.IsLoading);

            //ゲームシステムを起動
            StartSequence();
        }

        
        /// <summary>
        /// オープニングシーケンスを開始する
        /// </summary>
        private async void StartSequence()
        {
            await _openingSequence.PlaySequence();
        }


        public void OnInputEvent(OpeningSceneInputType inputType)
        {
            //入力応答が不可能なら処理を終わる
            if (!_canInteract) return;

            switch (inputType)
            {
                //ゲームを開始する処理
                case OpeningSceneInputType.StartGame:
                    //オープニングシーケンスに入力を通知
                    _openingSequence.NotifyGameStartInput();
                    _canInteract = false;
                    break;
                //エラーハンドリング
                default:
                    Debug.LogError($"未対応の入力タイプです: {inputType}");
                    break;
            }
        }
    }
}