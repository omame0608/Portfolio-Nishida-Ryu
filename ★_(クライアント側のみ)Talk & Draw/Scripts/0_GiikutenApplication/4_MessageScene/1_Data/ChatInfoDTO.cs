using System;
using UnityEngine;

namespace GiikutenApplication.MessageScene.Data
{
    /// <summary>
    /// メッセージ可能なユーザDTO
    /// </summary>
    [Serializable]
    public class ChatInfoDTO
    {
        // JsonUtility は Guid を直接扱えないので string に変更
        [SerializeField] private string _roomid; //ルームID
        [SerializeField] private string _userName; //ユーザ名
        [SerializeField] private string _imageURL; //アイコン画像のURL

        // コンストラクタ（必要ならGuidを文字列に変換）
        public ChatInfoDTO(Guid roomid, string userName, string imageURL)
        {
            _roomid = roomid.ToString();
            _userName = userName;
            _imageURL = imageURL;
        }

        // プロパティ一覧
        public Guid RoomID => Guid.TryParse(_roomid, out var g) ? g : Guid.Empty;
        public string UserName => _userName;
        public string ImageURL => _imageURL;
    }
}