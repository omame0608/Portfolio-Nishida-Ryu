using Cysharp.Threading.Tasks;
using GiikutenApplication.HomeScene.Domain.Model;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.HomeScene.Domain.Usecase
{
    /// <summary>
    /// HomeSceneをロードした際に行う初期化処理
    /// </summary>
    public class HomeSceneLoadUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IHomeSceneRepository _repository;
        [Inject] private readonly UserHeaderModel _userHeaderModel;
        

        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async UniTask Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:HomeScreenをロードした際に行う初期化処理");

            //サーバからDTOをもらう
            var dto = await _repository.Fetch();
            if (dto == null)
            {
                Debug.LogError($"データ受け取りエラー");
                return;
            }

            //ModelにDTOを適用
            _userHeaderModel.UserName.Value = dto.UserName;
            _userHeaderModel.JobName.Value = dto.JobName;
            _userHeaderModel.GachaStoneAmount.Value = dto.GachaStoneAmount;
        }
    }
}
