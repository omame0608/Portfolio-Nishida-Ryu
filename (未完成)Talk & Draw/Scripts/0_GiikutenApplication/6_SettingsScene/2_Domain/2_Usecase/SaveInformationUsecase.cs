using GiikutenApplication.SettingsScene.Domain.Model;
using System;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.SettingsScene.Domain.Usecase
{
    /// <summary>
    /// SettingsInDTOをサーバへ送信する処理
    /// </summary>
    public class SaveInformationUsecase : IDisposable
    {
        //対象のリポジトリ,Model
        [Inject] private readonly ISettingsSneneRepository _repository;
        [Inject] private readonly InformationModel _informationModel;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:SettingsInDTOをサーバへ送信する処理");

            //送信用データを作成
            var dto = _informationModel.ConvertModelToDTO();

            //データを送信
            var clear = await _repository.Save(dto);
            if (!clear) Debug.LogError($"サーバへの書き込みに失敗");
        }


        public  void Dispose()
        {
            Execute();
        }
    }
}
