namespace PerfectBackParkerRev.GameCores.Repositories
{
    /// <summary>
    /// セーブシステムの窓口インターフェース
    /// </summary>
    public interface ISaveSystemFacade
    {
        /// <summary>
        /// ステージのハイスコアを保存する
        /// </summary>
        /// <param name="stageKey">ステージキー</param>
        /// <param name="newHighScore">保存する値</param>
        void SaveStageHighScore(SaveKey stageKey, int newHighScore);

        /// <summary>
        /// ステージのハイスコアを読み込む
        /// </summary>
        /// <param name="stageKey">ステージキー</param>
        /// <returns>指定したステージのハイスコア</returns>
        int LoadStageHighScore(SaveKey stageKey);

        /// <summary>
        /// 全ステージのハイスコアの合計を取得する
        /// </summary>
        /// <returns>合計ハイスコア</returns>
        int GetTotalGameScore();
    }


    /// <summary>
    /// セーブ時に利用できるキー一覧
    /// </summary>
    public enum SaveKey
    {
        Stage1 = 1,
        Stage2 = 2,
        Stage3 = 3,
        Stage4 = 4,
        Stage5 = 5,
        None = 0,
    }
}