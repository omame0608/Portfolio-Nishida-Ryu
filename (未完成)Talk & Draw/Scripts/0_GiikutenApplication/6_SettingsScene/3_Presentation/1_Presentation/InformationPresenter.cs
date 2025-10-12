using GiikutenApplication.SettingsScene.Domain.Model;
using GiikutenApplication.SettingsScene.Domain.Usecase;
using GiikutenApplication.SettingsScene.Presentation.IView;
using System;
using UniRx;
using VContainer;

namespace GiikutenApplication.SettingsScene.Presentation.Presenter
{
    /// <summary>
    /// 設定画面で利用するデータPresenter
    /// </summary>
    public class InformationPresenter : IDisposable
    {
        //対象のModel,View,Usecase
        [Inject] private readonly InformationModel _informationModel;
        [Inject] private readonly IInformationView _informationView;
        [Inject] private readonly SaveInformationUsecase _saveInformationUsecase;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        /// <summary>
        /// イベント購読処理
        /// </summary>
        public void Init()
        {
            //ユーザ名
            _informationModel.UserName
                .Subscribe(text => _informationView.UpdateUserName(text))
                .AddTo(_disposables);

            //ジョブ名
            _informationModel.JobName
                .Subscribe(text => _informationView.UpdateJobName(text))
                .AddTo(_disposables);

            //血液型
            _informationModel.BloodType
                .Subscribe(text =>
                {
                    switch (text)
                    {
                        case "A": _informationView.UpdateBloodType(0); break;
                        case "B": _informationView.UpdateBloodType(1); break;
                        case "AB": _informationView.UpdateBloodType(2); break;
                        case "O": _informationView.UpdateBloodType(3); break;
                    }
                })
                .AddTo(_disposables);
            _informationView.OnBloodTypeChanged
                .Subscribe(id =>
                {
                    string data = "";
                    switch (id)
                    {
                        case 0: data = "A"; break;
                        case 1: data = "B"; break;
                        case 2: data = "AB"; break;
                        case 3: data = "O"; break;
                    }
                    _informationModel.BloodType.Value = data;
                })
                .AddTo(_disposables);

            //身長
            _informationModel.Height
                .Subscribe(num => _informationView.UpdateHeight(num - 140))
                .AddTo(_disposables);
            _informationView.OnHeightChanged
                .Subscribe(id => _informationModel.Height.Value = id + 140)
                .AddTo(_disposables);

            //誕生日
            _informationModel.Birthday
                .Subscribe(text =>
                {
                    if (text == null) return;
                    int y = int.Parse(text.Substring(0, 4));
                    int m = int.Parse(text.Substring(4, 2));
                    int d = int.Parse(text.Substring(6, 2));
                    _informationView.UpdateBirthdayY(2025 - y);
                    _informationView.UpdateBirthdayM(m - 1);
                    _informationView.UpdateBirthdayD(d - 1);
                })
                .AddTo(_disposables);
            _informationView.OnYearChanged
                .Subscribe(id =>
                {
                    var y = 2025 - id;
                    string data = y.ToString() + _informationModel.Birthday.Value.Substring(4, 4);
                    _informationModel.Birthday.Value = data;
                })
                .AddTo(_disposables);
            _informationView.OnMonthChanged
                .Subscribe(id =>
                {
                    var m = (id + 1).ToString("D2");
                    string data = _informationModel.Birthday.Value.Substring(0, 4)
                                    + m + _informationModel.Birthday.Value.Substring(6, 2);
                    _informationModel.Birthday.Value = data;
                })
                .AddTo(_disposables);
            _informationView.OnDayChanged
                .Subscribe(id =>
                {
                    var d = (id + 1).ToString("D2");
                    string data = _informationModel.Birthday.Value.Substring(0, 6) + d;
                    _informationModel.Birthday.Value = data;
                })
                .AddTo(_disposables);

            //好きな天気
            _informationModel.FavoriteWeather
                .Subscribe(text =>
                {
                    switch (text)
                    {
                        case "sunny": _informationView.UpdateWeather(0); break;
                        case "rainy": _informationView.UpdateWeather(1); break;
                        case "cloudy": _informationView.UpdateWeather(2); break;
                    }
                })
                .AddTo(_disposables);
            _informationView.OnWeatherChanged
                .Subscribe(id =>
                {
                    string data = "";
                    switch (id)
                    {
                        case 0: data = "sunny"; break;
                        case 1: data = "rainy"; break;
                        case 2: data = "cloudy"; break;
                    }
                    _informationModel.FavoriteWeather.Value = data;
                })
                .AddTo(_disposables);

            //好きな色
            _informationModel.FavoriteColor
                .Subscribe(text =>
                {
                    switch (text)
                    {
                        case "red": _informationView.UpdateColor(0); break;
                        case "blue": _informationView.UpdateColor(1); break;
                        case "green": _informationView.UpdateColor(2); break;
                        case "yellow": _informationView.UpdateColor(3); break;
                        case "purple": _informationView.UpdateColor(4); break;
                        case "black": _informationView.UpdateColor(5); break;
                        case "white": _informationView.UpdateColor(6); break;
                    }
                })
                .AddTo(_disposables);
            _informationView.OnColorChanged
                .Subscribe(id =>
                {
                    string data = "";
                    switch (id)
                    {
                        case 0: data = "red"; break;
                        case 1: data = "blue"; break;
                        case 2: data = "green"; break;
                        case 3: data = "yellow"; break;
                        case 4: data = "purple"; break;
                        case 5: data = "black"; break;
                        case 6: data = "white"; break;
                    }
                    _informationModel.FavoriteColor.Value = data;
                })
                .AddTo(_disposables);

            //利き手
            _informationModel.DominantHand
                .Subscribe(text =>
                {
                    switch (text)
                    {
                        case "left": _informationView.UpdateHand(0); break;
                        case "right": _informationView.UpdateHand(1); break;
                        case "ambidextrous": _informationView.UpdateHand(2); break;
                    }
                })
                .AddTo(_disposables);
            _informationView.OnHandChanged
                .Subscribe(id =>
                {
                    string data = "";
                    switch (id)
                    {
                        case 0: data = "left"; break;
                        case 1: data = "right"; break;
                        case 2: data = "ambidextrous"; break;
                    }
                    _informationModel.DominantHand.Value = data;
                })
                .AddTo(_disposables);

            //自己紹介
            _informationModel.Text
                .Subscribe(text => _informationView.UpdateText(text))
                .AddTo(_disposables);
            _informationView.OnTextEndEdit
                .Subscribe(text => _informationModel.Text.Value = text)
                .AddTo(_disposables);
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}