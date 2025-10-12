using GiikutenApplication.MessageScene.Presentation.IView;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.MessageScene.Domain.Usecase
{
    /// <summary>
    /// MessageSceneをロードした際に行う初期化処理
    /// </summary>
    public class MessageSceneLoadUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IMessageSceneRepository _messageSceneRepository;
        [Inject] private readonly ISelectUserView _userSelectView;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:MessageScreenをロードした際に行う初期化処理");

            //サーバからDTOをもらう
            var dtoArray = await _messageSceneRepository.FetchChatInfo();
            if (dtoArray == null)
            {
                Debug.LogError($"データ受け取りエラー");
                return;
            }

            //ここでチャット相手選択画面を構築
            _userSelectView.SetSelectUserView(dtoArray);
        }
    }
}