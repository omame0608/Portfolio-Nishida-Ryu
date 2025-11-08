using PerfectBackParkerRev.Databases;

namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ウェーブ開始ビューのインターフェース
    /// </summary>
    public interface IWaveStartView
    {
        /// <summary>
        /// ウェーブ開始時のインフォメーションUIを表示
        /// アニメーション終了後に非表示にする
        /// </summary>
        /// <param name="waveData">進行中のウェーブのデータ</param>
        /// <returns></returns>
        void ShowWaveInfomation(WaveData waveData);
    }
}