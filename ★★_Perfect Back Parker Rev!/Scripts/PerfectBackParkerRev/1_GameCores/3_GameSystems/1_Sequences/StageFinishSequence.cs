using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.Utilities;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems.Sequences
{
    /// <summary>
    /// ステージ終了のシーケンス
    /// 最終ウェーブ終了直後からステージ選択画面に遷移するまで
    /// </summary>
    public class StageFinishSequence
    {
        //システム
        [Inject] private readonly GoldenScrewStatus _goldenScrewStatus;
        [Inject] private readonly ScoreStatus _scoreStatus;
        [Inject] private readonly IStageFinishView _stageFinishView;
        [Inject] private readonly IStageFinishTransition _stageFinishTransition;
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;

        //Audio
        [Inject] private readonly BGMManager _bgmManager;

        //入力通知用
        private UniTaskCompletionSource<bool> _returnInputReceived;


        /// <summary>
        /// ステージ終了のシーケンスを再生する
        /// </summary>
        /// <param name="stageNumber">終了するステージ番号</param>
        /// <returns></returns>
        public async UniTask PlaySequence(int stageNumber)
        {
            MyLogger.Log($"<color=yellow>StageFinishSequenceを開始します</color>");

            //ステージスコアを取得
            var waveScores = _scoreStatus.GetStatus();
            var stageScore = _scoreStatus.StageTotalScore;

            //「金のネジ」取得状況を取得
            var goldenScrews = _goldenScrewStatus.GetStatus();

            //ハイスコア更新を保存
            var currentHighScore = _saveSystemFacade.LoadStageHighScore((SaveKey)stageNumber);
            if (stageScore > currentHighScore)
            {
                _saveSystemFacade.SaveStageHighScore((SaveKey)stageNumber, stageScore);
                MyLogger.Log($"<color=green>ハイスコア更新！{currentHighScore}=>{stageScore}</color>");
            }

            //ステージリザルト画面を表示
            await _stageFinishView.ShowStageResult(stageNumber, waveScores, stageScore, goldenScrews);

            //リターン入力を待機
            _returnInputReceived = new UniTaskCompletionSource<bool>();
            await _returnInputReceived.Task;
            _returnInputReceived = null;

            //BGMフェードアウト
            _bgmManager.StopBGM(true);

            //ステージ選択画面へ遷移する
            _stageFinishTransition.AwakeTransition((SceneType)(stageNumber + 1));
        }


        /// <summary>
        /// Enterキーまたはクリックの入力があったことを通知する
        /// 入力待機中でなければ無視される
        /// </summary>
        public void NotifyReturnInput()
        {
            //リターンの入力があったことを通知
            _returnInputReceived?.TrySetResult(true);
        }
    }
}