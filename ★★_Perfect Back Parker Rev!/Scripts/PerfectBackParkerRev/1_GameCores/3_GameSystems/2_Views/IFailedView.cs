namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ウェーブ失敗のビューのインターフェース
    /// </summary>
    public interface IFailedView
    {
        /// <summary>
        /// ウェーブ失敗時の演出を表示する
        /// </summary>
        void ShowFailedResult();

        /// <summary>
        /// ウェーブ失敗時の演出を非表示にする
        /// </summary>
        void HideFailedResult();
    }
}