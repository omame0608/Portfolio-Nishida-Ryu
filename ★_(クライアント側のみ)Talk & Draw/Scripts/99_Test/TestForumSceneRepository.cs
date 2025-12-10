using Cysharp.Threading.Tasks;
using GiikutenApplication.ForumScene.Data;
using GiikutenApplication.ForumScene.Domain;
using GiikutenApplication.MessageScene.Data;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.PackScene.Data;
using System;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：テストサーバとの通信を行う窓口
    /// あとから本番サーバとの通信を行う窓口に置き換える
    /// </summary>
    public class TestForumSceneRepository : IForumSceneRepository
    {
        //通信するサーバ
        //private TestVirtualServer _server = TestVirtualServer.Instance;

        public async UniTask<ForumDTO[]> FetchTopicInfo()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetForumDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestForumSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<MessageInDTO[]> FetchAllMessageInDTOForForum(Guid roomID)
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetAllMessageInDTOForForum(roomID);
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestForumSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<MessageInDTO> FetchMessageInDTOForForum(Guid roomID)
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetMessageInDTOForForum(roomID);
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestForumSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<bool> SaveMessageOutDTOForForum(MessageOutDTO dto)
        {
            try
            {
                var canSave = await TestVirtualServer.Instance.SetMessageOutDTOForForum(dto);
                return canSave;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestForumSceneRepositoryがサーバへの書き込みに失敗しました");
                return false;
            }
        }
    }
}
