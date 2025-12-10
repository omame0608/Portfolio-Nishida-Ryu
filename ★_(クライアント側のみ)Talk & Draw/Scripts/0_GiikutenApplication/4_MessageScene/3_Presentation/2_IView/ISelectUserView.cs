using GiikutenApplication.MessageScene.Data;

namespace GiikutenApplication.MessageScene.Presentation.IView
{
    /// <summary>
    /// Message画面の会話相手選択インターフェース
    /// </summary>
    public interface ISelectUserView
    {
        /// <summary>
        /// SelectUserViewを構築する
        /// </summary>
        /// <param name="dtoArray">会話可能なユーザ情報配列</param>
        void SetSelectUserView(ChatInfoDTO[] dtoArray);
    }
}