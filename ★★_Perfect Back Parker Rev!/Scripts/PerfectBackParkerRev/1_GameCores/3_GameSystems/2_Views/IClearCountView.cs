namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// クリアカウントビューのインターフェース
    /// </summary>
    public interface IClearCountView
    {
        /// <summary>
        /// サークル表示の可否
        /// falseセット時にCancelを実行し瞬時に非表示にする
        /// </summary>
        bool CanDisplayCircle { get; set; }

        /// <summary>
        /// カウントアニメーションを一周分表示する
        /// </summary>
        /// <param name="count">1,2,3カウントのいずれか</param>
        void ShowCountOnce(int count);

        /// <summary>
        /// サークルを非表示にする
        /// アニメーション中でもfalseにすると瞬時に非表示にできる
        /// </summary>
        void Cancel();
    }
}