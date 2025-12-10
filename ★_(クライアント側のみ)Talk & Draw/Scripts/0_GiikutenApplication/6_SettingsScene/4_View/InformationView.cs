using GiikutenApplication.SettingsScene.Presentation.IView;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.SettingsScene.View
{
    /// <summary>
    /// 設定画面で利用するデータView
    /// </summary>
    public class InformationView : MonoBehaviour, IInformationView
    {
        //各UI要素
        [SerializeField] private Text _userNameText;
        [SerializeField] private Text _jobNameText;
        [SerializeField] private TMP_Dropdown _bloodType;
        [SerializeField] private TMP_Dropdown _height;
        [SerializeField] private TMP_Dropdown _birthdayY;
        [SerializeField] private TMP_Dropdown _birthdayM;
        [SerializeField] private TMP_Dropdown _birthdayD;
        [SerializeField] private TMP_Dropdown _weather;
        [SerializeField] private TMP_Dropdown _color;
        [SerializeField] private TMP_Dropdown _hand;
        [SerializeField] private TMP_InputField _text;


        //ユーザ名
        public void UpdateUserName(string userName) => _userNameText.text = userName;

        //ジョブ名
        public void UpdateJobName(string jobName) => _jobNameText.text = jobName;

        //血液型
        public void UpdateBloodType(int id) => _bloodType.value = id;
        public IObservable<int> OnBloodTypeChanged => _bloodType.onValueChanged.AsObservable();

        //身長
        public void UpdateHeight(int id) => _height.value = id;
        public IObservable<int> OnHeightChanged => _height.onValueChanged.AsObservable();

        //誕生日：年
        public void UpdateBirthdayY(int id) => _birthdayY.value = id;
        public IObservable<int> OnYearChanged => _birthdayY.onValueChanged.AsObservable();

        //誕生日：月
        public void UpdateBirthdayM(int id) => _birthdayM.value = id;
        public IObservable<int> OnMonthChanged => _birthdayM.onValueChanged.AsObservable();

        //誕生日：日
        public void UpdateBirthdayD(int id) => _birthdayD.value = id;
        public IObservable<int> OnDayChanged => _birthdayD.onValueChanged.AsObservable();

        //好きな天気
        public void UpdateWeather(int id) => _weather.value = id;
        public IObservable<int> OnWeatherChanged => _weather.onValueChanged.AsObservable();

        //好きな色
        public void UpdateColor(int id) => _color.value = id;
        public IObservable<int> OnColorChanged => _color.onValueChanged.AsObservable();

        //利き手
        public void UpdateHand(int id) => _hand.value = id;
        public IObservable<int> OnHandChanged => _hand.onValueChanged.AsObservable();

        //自己紹介
        public void UpdateText(string text) => _text.text = text;
        public IObservable<string> OnTextEndEdit => _text.onEndEdit.AsObservable();
    }
}