namespace GiikutenApplication.HomeScene.Presentation
{
    /// <summary>
    /// 画面上部のユーザ情報を表示するヘッダーViewのインターフェース
    /// </summary>
    public interface IUserHeaderView
    {
        /// <summary>
        /// ヘッダー内のユーザ名を更新
        /// </summary>
        /// <param name="userName">更新後のユーザ名</param>
        void UpdateUserName(string userName);


        /// <summary>
        /// ヘッダー内のジョブ名を更新
        /// </summary>
        /// <param name="jobName">更新後のジョブ名</param>
        void UpdateJobName(string jobName);


        /// <summary>
        /// ヘッダー内のガチャ石の所持数を更新
        /// </summary>
        /// <param name="gachaStoneAmount">更新後のガチャ石の所持数</param>
        void UpdateGachaStoneAmount(int gachaStoneAmount);
    }
}