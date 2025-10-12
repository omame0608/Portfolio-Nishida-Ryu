using GiikutenApplication.SettingsScene.Data;
using UniRx;

namespace GiikutenApplication.SettingsScene.Domain.Model
{
    /// <summary>
    /// 設定画面で利用するデータModel
    /// </summary>
    public class InformationModel
    {
        //データ本体
        public readonly ReactiveProperty<string> UserName = new();
        public readonly ReactiveProperty<string> JobName = new();
        public readonly ReactiveProperty<string> BloodType = new(); //血液型
        public readonly ReactiveProperty<int> Height = new(); //身長
        public readonly ReactiveProperty<string> Birthday = new(); //誕生日
        public readonly ReactiveProperty<string> FavoriteWeather = new(); //好きな天気
        public readonly ReactiveProperty<string> FavoriteColor = new(); //好きな色
        public readonly ReactiveProperty<string> DominantHand = new(); //利き手
        public readonly ReactiveProperty<string> Text = new(); //自己紹介


        /// <summary>
        /// ModelをDTOに変換したものを作成
        /// </summary>
        /// <returns>変換したDTO</returns>
        public SettingsInDTO ConvertModelToDTO()
        {
            return new SettingsInDTO(UserName.Value, JobName.Value, BloodType.Value, Height.Value
                                        , Birthday.Value, FavoriteWeather.Value, FavoriteColor.Value
                                        , DominantHand.Value, Text.Value);
        }
    }
}