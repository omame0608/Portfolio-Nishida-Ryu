using System;

namespace GiikutenApplication.SettingsScene.Presentation.IView
{
    /// <summary>
    /// 設定画面で利用するデータViewのインターフェース
    /// </summary>
    public interface IInformationView
    {
        //ユーザ名
        void UpdateUserName(string userName);

        //ジョブ名
        void UpdateJobName(string jobName);

        //血液型
        void UpdateBloodType(int id);
        IObservable<int> OnBloodTypeChanged { get; }

        //身長
        void UpdateHeight(int id);
        IObservable<int> OnHeightChanged { get; }

        //誕生日：年
        void UpdateBirthdayY(int id);
        IObservable<int> OnYearChanged { get; }

        //誕生日：月
        void UpdateBirthdayM(int id);
        IObservable<int> OnMonthChanged { get; }

        //誕生日：日
        void UpdateBirthdayD(int id);
        IObservable<int> OnDayChanged { get; }

        //好きな天気
        void UpdateWeather(int id);
        IObservable<int> OnWeatherChanged { get; }

        //好きな色
        void UpdateColor(int id);
        IObservable<int> OnColorChanged { get; }

        //利き手
        void UpdateHand(int id);
        IObservable<int> OnHandChanged { get; }

        //自己紹介
        void UpdateText(string text);
        IObservable<string> OnTextEndEdit { get; }
    }
}