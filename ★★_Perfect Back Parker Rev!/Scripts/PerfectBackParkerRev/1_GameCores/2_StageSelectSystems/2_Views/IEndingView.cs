using Cysharp.Threading.Tasks;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Views
{
    /// <summary>
    /// エンディングビューのインターフェース
    /// </summary>
    public interface IEndingView
    {
        /// <summary>
        /// エンディングを表示する
        /// </summary>
        /// <param name="stageScores">各ステージのハイスコア</param>
        /// <param name="totalHighScore">合計スコア</param>
        /// <returns></returns>
        UniTask ShowEndingView(int[] stageScores, int totalHighScore);

        /// <summary>
        /// エンディングを終了する
        /// </summary>
        UniTask HideEndingView();
    }
}