using System;

namespace PerfectBackParkerRev.GameCores.GameSystems.Scores
{
    /// <summary>
    /// スコア計算を行うクラス
    /// </summary>
    public static class ScoreCalculator
    {
        /*
         * スコア計算式
         * ウェーブ基礎得点＝DBにあるウェーブ基礎得点そのまま
         * クリア時間得点　＝(残り時間/制限時間) × ウェーブ基礎得点 × 2
         * ノーミスボーナス＝ウェーブ基礎得点 × 2
         * 金のネジボーナス＝500点固定
         * 
         * 参考：一秒あたりの得点
         * 制限時間120秒、基礎得点200点の場合：3.33点/秒
         * 制限時間120秒、基礎得点500点の場合：8.33点/秒
         * 制限時間120秒、基礎得点1000点の場合：16.66点/秒
         * 
         * ・多少の時間を使ってでも金のネジを取った方が基本的には得になるようにしたい
         * ・ノーミスボーナスは高めに設定し、ミスを減らすインセンティブを強化
         * ・基礎得点1900で理論値10000点、これ未満で4桁に抑える
         */


        //スコア計算用定数
        private const int _WAVE_SCREW_BONUS = 500; //金のネジボーナス


        /// <summary>
        /// ウェーブクリア時のスコアを計算する
        /// </summary>
        /// <param name="waveBaseScore">ウェーブ基礎得点</param>
        /// <param name="timeLimit">ウェーブの制限時間</param>
        /// <param name="remainingTime">ウェーブクリア時の残り時間</param>
        /// <param name="isNoMissClear">ノーミスクリアかどうか</param>
        /// <param name="waveScrewResult">金のネジを取得してのクリアか</param>
        /// <returns>スコア群</returns>
        public static WaveScores CalculateWaveScore(int waveBaseScore,
                                              int timeLimit,
                                              int remainingTime,
                                              bool isNoMissClear,
                                              bool waveScrewResult)
        {
            //各種要素スコアを計算、基礎得点は引数そのまま
            int timeScore = (int)((float)remainingTime / timeLimit * waveBaseScore * 2);
            int noMissScore = isNoMissClear ? waveBaseScore * 2 : 0;
            int goldenScrewScore = waveScrewResult ? _WAVE_SCREW_BONUS : 0;

            //合計スコアを計算
            int waveTotalScore = waveBaseScore + timeScore + noMissScore + goldenScrewScore;

            //結果をまとめて返す
            return new WaveScores(waveBaseScore, timeScore, noMissScore, goldenScrewScore, waveTotalScore);
        }
    }


    /// <summary>
    /// ウェーブクリア時の各種スコア結果
    /// </summary>
    public class WaveScores
    {
        //保持するデータ一覧
        private readonly int _waveBaseScore;
        private readonly int _timeScore;
        private readonly int _noMissScore;
        private readonly int _goldenScrewScore;
        private readonly int _waveTotalScore;


        public WaveScores(int waveBaseScore, int timeScore, int noMissScore, int goldenScrewScore, int waveTotalScore)
        {
            _waveBaseScore = waveBaseScore;
            _timeScore = timeScore;
            _noMissScore = noMissScore;
            _goldenScrewScore = goldenScrewScore;
            _waveTotalScore = waveTotalScore;
        }


        //各種プロパティ
        public int WaveBaseScore => _waveBaseScore;
        public int TimeScore => _timeScore;
        public int NoMissScore => _noMissScore;
        public int GoldenScrewScore => _goldenScrewScore;
        public int WaveTotalScore => _waveTotalScore;
    }
}