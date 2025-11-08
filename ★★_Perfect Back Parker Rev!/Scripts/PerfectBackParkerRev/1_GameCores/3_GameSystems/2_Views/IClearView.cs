using PerfectBackParkerRev.GameCores.GameSystems.Scores;

namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// クリアビューのインターフェース
    /// </summary>
    public interface IClearView
    {
        /// <summary>
        /// ウェーブ成功時の演出を表示する
        /// </summary>
        /// <param name="scores">スコアDTO</param>
        void ShowClearResult(WaveScores scores);

        /// <summary>
        /// ウェーブ成功時の演出を非表示にする
        /// </summary>
        /// <param name="isFinalWave">最終ウェーブかどうか</param>
        void HideClearResult(bool isFinalWave);
    }
}