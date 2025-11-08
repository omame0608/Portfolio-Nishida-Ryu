using Cysharp.Threading.Tasks;

namespace PerfectBackParkerRev.GameCores.OpeningSystems.Views
{
    /// <summary>
    /// オープニング画面表示用インターフェース
    /// </summary>
    public interface IOpeningView
    {
        /// <summary>
        /// オープニング演出用のビューを再生する
        /// </summary>
        void PlayOpeningView();

        /// <summary>
        /// オープニング演出用のビューを終了する
        /// </summary>
        UniTask FinishOpeningView();
    }
}