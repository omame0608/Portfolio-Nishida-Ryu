using System;

namespace GiikutenApplication.ForumScene.Data
{
    /// <summary>
    /// 参加可能なトピックDTO
    /// </summary>
    public class ForumDTO
    {
        //会話相手選択画面に表示するデータ一覧
        private readonly Guid _roomid; //ルームID
        private readonly string _userName; //ユーザ名
        private readonly string _imageURL; //アイコン画像のURL
        private readonly string _title; //タイトル


        public ForumDTO(Guid roomid, string userName, string imageURL, string title)
        {
            _roomid = roomid;
            _userName = userName;
            _imageURL = imageURL;
            _title = title;
        }


        //プロパティ一覧
        public Guid RoomID => _roomid;
        public string UserName => _userName;
        public string ImageURL => _imageURL;
        public string Title => _title;
    }

}