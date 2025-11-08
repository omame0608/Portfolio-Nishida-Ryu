using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using PerfectBackParkerRev.Utilities;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Sequences
{
    /// <summary>
    /// エンディングシーケンス
    /// </summary>
    public class EndingSequence
    {
        //システム
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;
        [Inject] private readonly IEndingView _endingView;

        //入力通知用
        private UniTaskCompletionSource<bool> _gameBackInputReceived;

        /// <summary>
        /// エンディングシーケンスを再生する
        /// </summary>
        public async UniTask PlaySequence()
        {
            Debug.Log($"<color=yellow>エンディングシーケンス再生</color>");

            //各ステージのハイスコアと総合スコアを取得
            int[] stageScores = new int[3];
            stageScores[0] = _saveSystemFacade.LoadStageHighScore(SaveKey.Stage1);
            stageScores[1] = _saveSystemFacade.LoadStageHighScore(SaveKey.Stage2);
            stageScores[2] = _saveSystemFacade.LoadStageHighScore(SaveKey.Stage3);
            int totalHighScore = _saveSystemFacade.GetTotalGameScore();

            //エンディングビューを再生
            await _endingView.ShowEndingView(stageScores, totalHighScore);

            //ゲームに戻る入力を待機
            MyLogger.Log($"受付待機開始");
            _gameBackInputReceived = new UniTaskCompletionSource<bool>();
            await _gameBackInputReceived.Task;
            _gameBackInputReceived = null;
            MyLogger.Log($"受付待機終了");


            //エンディングビューを終了
            await _endingView.HideEndingView();
        }


        /// <summary>
        /// ゲームに戻る入力があったことを通知する
        /// 入力待機中でなければ無視される
        /// </summary>
        public void NotifyGameBackInput()
        {
            //ゲームスタートの入力があったことを通知
            _gameBackInputReceived?.TrySetResult(true);
        }
    }
}