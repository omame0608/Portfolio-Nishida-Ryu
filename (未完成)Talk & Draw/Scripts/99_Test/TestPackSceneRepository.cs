using Cysharp.Threading.Tasks;
using GiikutenApplication.PackScene.Data;
using GiikutenApplication.PackScene.Domain;
using System;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：テストサーバとの通信を行う窓口
    /// あとから本番サーバとの通信を行う窓口に置き換える
    /// </summary>
    public class TestPackSceneRepository : IPackSceneRepository
    {
        //通信するサーバ
        //private TestVirtualServer _server = TestVirtualServer.Instance;

        public async UniTask<PackScreenDTO> Fetch()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetPackScreenDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestPackSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<bool> Save(PackScreenDTO dto)
        {
            try
            {
                var canSave = await TestVirtualServer.Instance.SetPackScreenDTO(dto);
                return canSave;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestPackSceneRepositoryがサーバへの書き込みに失敗しました");
                return false;
            }
        }


        public async UniTask<RandomPackDTO> FetchRandomPack()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetRandomPackDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestPackSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<RecommendPackDTO> FetchRecommendPack()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetRecommendPackDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestPackSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }
    }
}
