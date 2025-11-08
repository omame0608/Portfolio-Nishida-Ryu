using PerfectBackParkerRev.Utilities;
using System.Collections.Generic;

namespace PerfectBackParkerRev.GameCores.GameSystems.Scores
{
    /// <summary>
    /// スコアの取得情報を保持する
    /// </summary>
    public class ScoreStatus
    {
        //各ウェーブの総スコア
        private List<WaveScores> _waveTotalScoreStatusList = new();

        //全ウェーブ総スコア＝ステージスコア
        public int StageTotalScore
        {
            get
            {
                int totalScore = 0;
                foreach (var waveScore in _waveTotalScoreStatusList)
                {
                    MyLogger.Log($"{waveScore.WaveTotalScore}");
                    totalScore += waveScore.WaveTotalScore;
                }
                return totalScore;
            }
        }


        /// <summary>
        /// スコアオブジェクトを保持する
        /// </summary>
        /// <param name="waveScores">保持するスコアオブジェクト</param>
        public void SetWaveScore(WaveScores waveScores)
        {
            _waveTotalScoreStatusList.Add(waveScores);
        }


        /// <summary>
        /// 各ウェーブのスコアを取得
        /// </summary>
        /// <returns>各ウェーブのスコア情報</returns>
        public List<WaveScores> GetStatus()
        {
            return _waveTotalScoreStatusList;
        }
    }
}