using GiikutenApplication.ForumScene.Presentation.IView;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.MessageScene.Presentation.IView;
using UnityEngine;
using VContainer;

namespace GiikutenApplication.ForumScene.Domain.Usecase
{
    /// <summary>
    /// ForumSceneをロードした際に行う初期化処理
    /// </summary>
    public class ForumSceneLoadUsecase
    {
        //対象のリポジトリ,Model
        [Inject] private readonly IForumSceneRepository _forumSceneRepository;
        [Inject] private readonly ISelectTopicView _topicSelectView;


        /// <summary>
        /// Usecase処理を実行
        /// </summary>
        public async void Execute()
        {
            Debug.Log($"<color=yellow>Usecase</color>:ForumScreenをロードした際に行う初期化処理");

            //サーバからDTOをもらう
            var dtoArray = await _forumSceneRepository.FetchTopicInfo();
            if (dtoArray == null)
            {
                Debug.LogError($"データ受け取りエラー");
                return;
            }

            //ここでチャット相手選択画面を構築
            _topicSelectView.SetSelectTopicView(dtoArray);
        }
    }
}