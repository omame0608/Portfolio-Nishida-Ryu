using Cysharp.Threading.Tasks;
using SupernovaTrolley.GameCores.Sequences;
using UnityEngine.SceneManagement;
using Utilities;
using VContainer;

namespace SupernovaTrolley.GameCores
{
    /// <summary>
    /// ゲームシステム
    /// </summary>
    public class GameSystem
    {
        //システム
        [Inject] private readonly OpeningSequence _openingSequence;
        [Inject] private readonly GameSequence _gameSequence;
        [Inject] private readonly EndingSequence _endingSequence;


        public async void Initialize()
        {
            await UniTask.Yield();
            MyLogger.Log($"<color=green>ゲームシステム起動</color>");
            await StartSequences();
            MyLogger.Log($"<color=green>ゲームシステム終了</color>");
            ReloadGameScene();
        }


        /// <summary>
        /// シーケンスを順に再生する
        /// </summary>
        private async UniTask StartSequences()
        {
            //WIP
            await _openingSequence.PlaySequence();
            await _gameSequence.PlaySequence();
            await _endingSequence.PlaySequence();
        }


        /// <summary>
        /// ゲームシーンをリロードする
        /// </summary>
        private void ReloadGameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}