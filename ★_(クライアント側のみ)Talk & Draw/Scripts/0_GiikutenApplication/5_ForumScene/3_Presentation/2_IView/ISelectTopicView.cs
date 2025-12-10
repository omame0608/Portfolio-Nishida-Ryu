using GiikutenApplication.ForumScene.Data;

namespace GiikutenApplication.ForumScene.Presentation.IView
{
    /// <summary>
    /// Forum画面の会話相手選択インターフェース
    /// </summary>
    public interface ISelectTopicView
    {
        /// <summary>
        /// SelectTopicViewを構築する
        /// </summary>
        /// <param name="dtoArray">会話可能なtopic情報配列</param>
        void SetSelectTopicView(ForumDTO[] dtoArray);
    }
}