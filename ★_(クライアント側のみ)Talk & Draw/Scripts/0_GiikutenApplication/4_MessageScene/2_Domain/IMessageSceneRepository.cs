using Cysharp.Threading.Tasks;
using GiikutenApplication.MessageScene.Data;
using System;

namespace GiikutenApplication.MessageScene.Domain
{
    /// <summary>
    /// MessageSceneとサーバを繋ぐ窓口インターフェース
    /// </summary>
    public interface IMessageSceneRepository
    {
        /// <summary>
        /// サーバから会話可能なユーザ一覧を読み込む
        /// </summary>
        /// <returns>サーバから読み込んだ会話可能なユーザの情報配列</returns>
        UniTask<ChatInfoDTO[]> FetchChatInfo();


        /// <summary>
        /// サーバから指定したユーザとの会話履歴を全て読み込む
        /// </summary>
        /// <param name="roomID">会話相手を指定するRoomID</param>
        /// <returns>サーバから読み込んだ会話情報配列</returns>
        UniTask<MessageInDTO[]> FetchAllMessageInDTO(Guid roomID);


        /// <summary>
        /// サーバから指定したユーザとの会話をひとつ
        /// </summary>
        /// <param name="roomID">会話相手を指定するRoomID</param>
        /// <returns>サーバから読み込んだ会話情報</returns>
        UniTask<MessageInDTO> FetchMessageInDTO(Guid roomID);


        /// <summary>
        /// サーバへユーザが入力したメッセージを送信する
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        UniTask<bool> SaveMessageOutDTO(MessageOutDTO dto);
    }
}
