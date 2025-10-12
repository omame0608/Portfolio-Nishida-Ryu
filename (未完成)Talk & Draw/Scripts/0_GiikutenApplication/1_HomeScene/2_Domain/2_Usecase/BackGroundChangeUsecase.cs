using GiikutenApplication.HomeScene.Domain.Model;
using GiikutenApplication.HomeScene.Presentation.IView;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.HomeScene.Domain.Usecase
{
    /// <summary>
    /// 背景画像の変更処理
    /// </summary>
    class BackGroundChangeUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IBackGroundView _backGroundView;
        [Inject] private readonly UserHeaderModel _userHeaderModel;

        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:背景画像の変更処理");

            //ジョブ名を取得し背景を変更
            string job = _userHeaderModel.JobName.Value;
            _backGroundView.SetBackGroundImage(job);
        }
    }
}
