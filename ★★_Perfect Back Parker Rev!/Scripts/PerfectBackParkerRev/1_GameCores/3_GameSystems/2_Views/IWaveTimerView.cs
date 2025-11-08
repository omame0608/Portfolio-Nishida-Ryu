namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ウェーブタイマーのビューインターフェース
    /// </summary>
    public interface IWaveTimerView
    {
        /// <summary>
        /// ウェーブタイマーを表示する
        /// </summary>
        void ShowWaveTimer();

        /// <summary>
        /// ウェーブタイマーを非表示にする
        /// </summary>
        void HideWaveTimer();

        /// <summary>
        /// ウェーブタイマーの残り時間を更新する
        /// </summary>
        /// <param name="time">表示する残り時間</param>
        void UpdateWaveTimer(int time);
    }
}