using GiikutenApplication.SettingsScene.Domain.Model;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.SettingsScene.Domain.Usecase
{
    /// <summary>
    /// SettingsSceneをロードした際に行う初期化処理
    /// </summary>
    public class SettingsSceneLoadUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly ISettingsSneneRepository _repository;
        [Inject] private readonly InformationModel _informationModel;

        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:SettingsScreenをロードした際に行う初期化処理");

            //サーバからDTOをもらう
            var dto = await _repository.Fetch();
            if (dto == null)
            {
                Debug.LogError($"データ受け取りエラー");
                return;
            }

            //ModelにDTOを適用
            _informationModel.UserName.Value = dto.UserName;
            _informationModel.JobName.Value = dto.JobName;
            _informationModel.BloodType.Value = dto.BloodType;
            _informationModel.Height.Value = dto.Height;
            _informationModel.Birthday.Value = dto.Birthday;
            _informationModel.FavoriteWeather.Value = dto.FavoriteWeather;
            _informationModel.FavoriteColor.Value = dto.FavoriteColor;
            _informationModel.DominantHand.Value = dto.DominantHand;
            _informationModel.Text.Value = dto.Text;
        }
    }
}