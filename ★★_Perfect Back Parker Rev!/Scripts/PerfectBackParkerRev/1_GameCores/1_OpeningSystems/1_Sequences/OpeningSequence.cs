using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.OpeningSystems.Views;
using VContainer;

namespace PerfectBackParkerRev.GameCores.OpeningSystems.Sequences
{
    /// <summary>
    /// オープニングシーケンス
    /// </summary>
    public class OpeningSequence
    {
        //システム
        [Inject] private readonly IOpeningView _openingView;
        [Inject] private readonly IGameStartTransition _gameStartTransition;

        //Audio
        [Inject] private readonly BGMManager _bgmManager;

        //入力通知用
        private UniTaskCompletionSource<bool> _gameStartInputReceived;


        /// <summary>
        /// オープニングシーケンスを再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask PlaySequence()
        {
            //オープニングビューを表示
            _openingView.PlayOpeningView();

            //BGM再生
            _bgmManager.PlayBGM(BGM.Opening);

            //ゲームスタートの入力を待機
            _gameStartInputReceived = new UniTaskCompletionSource<bool>();
            await _gameStartInputReceived.Task;
            _gameStartInputReceived = null;

            //オープニングビューを終了しシーンを遷移する
            await _openingView.FinishOpeningView();
            _gameStartTransition.AwakeTransition();

            //BGMフェードアウト
            _bgmManager.StopBGM(true);
        }


        /// <summary>
        /// ゲームスタートの入力があったことを通知する
        /// 入力待機中でなければ無視される
        /// </summary>
        public void NotifyGameStartInput()
        {
            //ゲームスタートの入力があったことを通知
            _gameStartInputReceived?.TrySetResult(true);
        }
    }
}