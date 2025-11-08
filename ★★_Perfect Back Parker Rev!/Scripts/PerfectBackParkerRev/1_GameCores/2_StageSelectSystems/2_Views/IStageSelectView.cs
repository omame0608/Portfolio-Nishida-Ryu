using Cysharp.Threading.Tasks;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Views
{
    /// <summary>
    /// ステージ選択画面の共通UIのインターフェース
    /// (現在選択しているステージに依らないUI)
    /// </summary>
    public interface IStageSelectView
    {
        /// <summary>
        /// ステージ選択画面の共通UIを表示する
        /// </summary>
        /// <param name="currentStageNumber">現在選択中のステージ番号</param>
        /// <param name="totalGameScore">総ゲームスコア</param>
        /// <param name="currentLatestStageNumber">現在遊べる最新のステージ</param>
        /// <returns></returns>
        UniTask ShowStageSelectView(int currentStageNumber, int totalGameScore, int currentLatestStageNumber);
    }
}