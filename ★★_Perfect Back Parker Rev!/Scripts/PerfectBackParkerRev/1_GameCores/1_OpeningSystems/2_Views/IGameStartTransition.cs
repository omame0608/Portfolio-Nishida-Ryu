namespace PerfectBackParkerRev.GameCores.OpeningSystems.Views
{
    /// <summary>
    /// ゲームスタートのトランジションインターフェース
    /// </summary>
    public interface IGameStartTransition
    {
        /// <summary>
        /// オープニング画面からステージ選択画面へ遷移するトランジションを再生する
        /// </summary>
        void AwakeTransition();
    }
}