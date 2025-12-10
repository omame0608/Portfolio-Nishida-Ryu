using System;
using UnityEngine;

namespace GiikutenApplication.SettingsScene.Data
{
    /// <summary>
    /// SettingsScene構築用のDTO
    /// </summary>
    [Serializable]
    public class SettingsInDTO
    {
        //SettingsScene構築に利用できるデータ一覧
        [SerializeField] private string _userName; //ユーザ名
        [SerializeField] private string _jobName; //ジョブ名
        [SerializeField] private string _bloodType; //血液型
        [SerializeField] private int _height; //身長
        [SerializeField] private string _birthday; //誕生日
        [SerializeField] private string _favoriteWeather; //好きな天気
        [SerializeField] private string _favoriteColor; //好きな色
        [SerializeField] private string _dominantHand; //利き手
        [SerializeField] private string _text; //自己紹介


        public SettingsInDTO(){}
        public SettingsInDTO(string userName, string jobName, string bloodType, int height, string birthday, string weather, string color, string hand, string text)
        {
            _userName = userName;
            _jobName = jobName;
            _bloodType = bloodType;
            _height = height;
            _birthday = birthday;
            _favoriteWeather = weather;
            _favoriteColor = color;
            _dominantHand = hand;
            _text = text;
        }

        //プロパティ一覧
        public string UserName => _userName;
        public string JobName => _jobName;
        public string BloodType => _bloodType;
        public int Height => _height;
        public string Birthday => _birthday;
        public string FavoriteWeather => _favoriteWeather;
        public string FavoriteColor => _favoriteColor;
        public string DominantHand => _dominantHand;
        public string Text => _text;
    }
}