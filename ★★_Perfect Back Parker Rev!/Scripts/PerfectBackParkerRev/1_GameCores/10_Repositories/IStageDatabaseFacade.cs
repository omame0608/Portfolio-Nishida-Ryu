using PerfectBackParkerRev.Databases;

namespace PerfectBackParkerRev.GameCores.Repositories
{
    /// <summary>
    /// ステージDB窓口のインターフェース
    /// </summary>
    public interface IStageDatabaseFacade
    {
        /// <summary>
        /// ステージ番号からステージ情報を取得する
        /// </summary>
        /// <param name="number">ステージ番号</param>
        /// <returns>ステージ情報</returns>
        StageData GetInfoWithStageNumber(int number);
    }
}