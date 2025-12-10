namespace GiikutenApplication.HomeScene.Presentation.IView
{
    /// <summary>
    /// HomeSceneの背景Viewインターフェース    
    /// </summary>
    public interface IBackGroundView
    {
        /// <summary>
        /// ユーザのジョブに合わせて背景画像を変更する
        /// </summary>
        /// <param name="jobName">ジョブ名</param>
        void SetBackGroundImage(string jobName);
    }
}
