using Cysharp.Threading.Tasks;
using SupernovaTrolley.Directions;
using SupernovaTrolley.GameCores.Statuses;
using SupernovaTrolley.HUDs;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SocialPlatforms.Impl;
using Utilities;
using VContainer;

namespace SupernovaTrolley.GameCores.Sequences
{
    /// <summary>
    /// エンディングシーケンス
    /// </summary>
    public class EndingSequence
    {
        //システム
        [Inject] private readonly RailScrollSystem _railScrollSystem;
        [Inject] private readonly EndingView _endingView;
        [Inject] private readonly StatusView _statusView;
        [Inject] private readonly ScoreStatus _scoreStatus;

        private bool _gameEndFlag = false;


        /// <summary>
        /// エンディングシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask PlaySequence()
        {
            MyLogger.Log($"<color=yellow>エンディングシーケンス再生</color>");

            //レールスクロール停止
            _railScrollSystem.releaseRailScrollSystem();

            //ステータスビューを非表示
            _statusView.HideStatusView();

            //一定時間待機後にリザルトを表示
            await UniTask.Delay(System.TimeSpan.FromSeconds(3f));

            //エンディングビューを表示
            var score = _scoreStatus.CurrentScore;
            _endingView.ShowEndingView(score);
            //_endingView.ShowEndingView(55555);

            //ゲーム終了フラグが立つまで待機
            await UniTask.WaitUntil(() => _gameEndFlag);
        }


        /// <summary>
        /// ゲーム終了フラグを立てる
        /// </summary>
        public void SetGameEndFlag()
        {
            _gameEndFlag = true;
        }
    }
}