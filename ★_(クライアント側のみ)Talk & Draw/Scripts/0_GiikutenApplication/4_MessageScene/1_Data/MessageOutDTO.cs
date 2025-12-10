using System;
using System.Xml.Linq;
using UnityEngine;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージを1つ送信するDTO
    /// </summary>
    [Serializable]
    public class MessageOutDTO
    {
        //メッセージのためにサーバに送信するデータ一覧
        [SerializeField]private string _roomID;
        [SerializeField]private string _message;


        public MessageOutDTO(Guid roomID, string message)
        {
            _roomID = roomID.ToString();
            _message = message;
        }


        public Guid RoomID => Guid.TryParse(_roomID, out var g) ? g : Guid.Empty;
        public string Message => _message;
    }
}
