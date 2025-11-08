using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using System.Collections.Generic;

namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ステージクリア画面のViewインターフェース
    /// </summary>
    public interface IStageFinishView
    {
        /// <summary>
        /// ステージのリザルトを表示する
        /// </summary>
        /// <param name="stageNumber">ステージ番号</param>
        /// <param name="waveScores">各ウェーブのスコア情報</param>
        /// <param name="stageScore">ステージスコア</param>
        /// <param name="goldenScrews">金のネジ取得情報</param>
        /// <returns></returns>
        UniTask ShowStageResult(int stageNumber, List<WaveScores> waveScores, int stageScore, bool?[] goldenScrews);
    }
}