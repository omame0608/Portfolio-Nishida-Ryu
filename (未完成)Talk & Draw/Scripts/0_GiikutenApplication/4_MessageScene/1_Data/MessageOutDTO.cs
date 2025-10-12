using System;
using System.Xml.Linq;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージを1つ送信するDTO
    /// </summary>
    public class MessageOutDTO
    {
        //メッセージのためにサーバに送信するデータ一覧
        private readonly Guid _roomID;
        private readonly string _message;


        public MessageOutDTO(Guid roomID, string message)
        {
            _roomID = roomID;
            _message = message;
        }


        public Guid RoomID => _roomID;
        public string Message => _message;
    }
}
