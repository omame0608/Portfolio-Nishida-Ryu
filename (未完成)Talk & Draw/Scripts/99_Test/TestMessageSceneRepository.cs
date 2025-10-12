using Cysharp.Threading.Tasks;
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
    public class TestMessageSceneRepository : IMessageSceneRepository
    {
        //通信するサーバ
        //private TestVirtualServer _server = TestVirtualServer.Instance;

        public async UniTask<ChatInfoDTO[]> FetchChatInfo()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetChatInfoDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestMessageSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<MessageInDTO[]> FetchAllMessageInDTO(Guid roomID)
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetAllMessageInDTO(roomID);
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestMessageSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<MessageInDTO> FetchMessageInDTO(Guid roomID)
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetMessageInDTO(roomID);
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestMessageSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<bool> SaveMessageOutDTO(MessageOutDTO dto)
        {
            try
            {
                var canSave = await TestVirtualServer.Instance.SetMessageOutDTO(dto);
                return canSave;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestMessageSceneRepositoryがサーバへの書き込みに失敗しました");
                return false;
            }
        }
    }
}
