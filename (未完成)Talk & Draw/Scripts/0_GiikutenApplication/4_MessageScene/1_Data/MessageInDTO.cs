using System;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージ一つを受け取るためのDTO
    /// </summary>
    public class MessageInDTO
    {
        //メッセージのためにサーバから受け取るデータ一覧
        private readonly Guid _messageID;
        private readonly string _message;
        private readonly string _imageURL;
        private readonly bool _isPerson;
        private readonly string _userName;


        public MessageInDTO(Guid messageID, string message, string imageURL, bool isPerson, string userName)
        {
            _messageID = messageID;
            _message = message;
            _imageURL = imageURL;
            _isPerson = isPerson;
            _userName = userName;
        }


        //プロパティ一覧
        public Guid MessageID => _messageID;
        public string Message => _message;
        public string ImageURL => _imageURL;
        public bool IsPerson => _isPerson;
        public string UserName => _userName;
    }
}
