using System;
using UnityEngine;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージ一つを受け取るためのDTO
    /// </summary>
    [Serializable]
    public class MessageInDTO
    {
        // JsonUtility は Guid を直接扱えないので string に変更
        [SerializeField] private string _messageID;
        [SerializeField] private string _message;
        [SerializeField] private string _imageURL;
        [SerializeField] private bool _isPerson;
        [SerializeField] private string _userName;

        // コンストラクタ（Guid を string に変換して保存）
        public MessageInDTO(Guid messageID, string message, string imageURL, bool isPerson, string userName)
        {
            _messageID = messageID.ToString();
            _message = message;
            _imageURL = imageURL;
            _isPerson = isPerson;
            _userName = userName;
        }

        // プロパティ一覧
        public Guid MessageID => Guid.TryParse(_messageID, out var g) ? g : Guid.Empty;
        public string Message => _message;
        public string ImageURL => _imageURL;
        public bool IsPerson => _isPerson;
        public string UserName => _userName;
    }
}