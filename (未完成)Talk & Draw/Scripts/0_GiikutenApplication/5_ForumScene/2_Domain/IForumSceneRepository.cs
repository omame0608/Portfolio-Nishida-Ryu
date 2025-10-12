using Cysharp.Threading.Tasks;
using GiikutenApplication.ForumScene.Data;
using GiikutenApplication.MessageScene.Data;
using System;

namespace GiikutenApplication.ForumScene.Domain
{
    /// <summary>
    /// ForumSceneとサーバを繋ぐ窓口インターフェース
    /// </summary>
    public interface IForumSceneRepository
    {
        /// <summary>
        /// サーバから選択可能なトピック一覧を読み込む
        /// </summary>
        /// <returns>サーバから読み込んだ選択可能なトピックの情報配列</returns>
        UniTask<ForumDTO[]> FetchTopicInfo();


        /// <summary>
        /// サーバから指定したトピックの会話履歴を全て読み込む
        /// </summary>
        /// <param name="roomID">トピックを指定するRoomID</param>
        /// <returns>サーバから読み込んだ会話情報配列</returns>
        UniTask<MessageInDTO[]> FetchAllMessageInDTOForForum(Guid roomID);


        /// <summary>
        /// サーバから指定したトピックとの会話をひとつ
        /// </summary>
        /// <param name="roomID">トピックを指定するRoomID</param>
        /// <returns>サーバから読み込んだ会話情報</returns>
        UniTask<MessageInDTO> FetchMessageInDTOForForum(Guid roomID);


        /// <summary>
        /// サーバへユーザが入力したメッセージを送信する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        UniTask<bool> SaveMessageOutDTOForForum(MessageOutDTO dto);
    }
}
