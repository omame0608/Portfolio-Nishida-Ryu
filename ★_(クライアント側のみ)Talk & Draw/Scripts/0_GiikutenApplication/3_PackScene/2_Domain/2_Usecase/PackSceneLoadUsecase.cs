using GiikutenApplication.PackScene.Domain.Model;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.PackScene.Domain.Usecase
{
    /// <summary>
    /// PackSceneをロードした際に行う初期化処理
    /// </summary>
    public class PackSceneLoadUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IPackSceneRepository _repository;
        [Inject] private readonly PackScreenModel _packScreenModel;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:PackScreenをロードした際に行う初期化処理");

            //サーバからDTOをもらう
            var dto = await _repository.Fetch();
            if (dto == null)
            {
                Debug.LogError($"データ受け取りエラー");
                return;
            }

            //ModelにDTOを適用
            _packScreenModel.GachaStoneAmount.Value = dto.GachaStoneAmount;
        }
    }
}