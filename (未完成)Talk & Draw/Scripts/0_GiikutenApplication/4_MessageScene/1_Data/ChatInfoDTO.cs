using System;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージ可能なユーザDTO
    /// </summary>
    public class ChatInfoDTO
    {
        //会話相手選択画面に表示するデータ一覧
        private readonly Guid _roomid; //ルームID
        private readonly string _userName; //ユーザ名
        private readonly string _imageURL; //アイコン画像のURL


        public ChatInfoDTO(Guid roomid, string userName, string imageURL)
        {
            _roomid = roomid;
            _userName = userName;
            _imageURL = imageURL;
        }


        //プロパティ一覧
        public Guid RoomID => _roomid;
        public string UserName => _userName;
        public string ImageURL => _imageURL;
    }

}