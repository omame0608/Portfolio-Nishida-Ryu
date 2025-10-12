using Cysharp.Threading.Tasks;
using GiikutenApplication.HomeScene.Data;
using GiikutenApplication.HomeScene.Domain;
using System;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：テストサーバとの通信を行う窓口
    /// あとから本番サーバとの通信を行う窓口に置き換える
    /// </summary>
    public class TestHomeSceneRepository : IHomeSceneRepository
    {
        //通信するサーバ
        //private TestVirtualServer _server = TestVirtualServer.Instance;


        public async UniTask<HomeSceneDTO> Fetch()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetHomeSceneDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestHomeSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<bool> Save(HomeSceneDTO dto)
        {
            try
            {
                var canSave = await TestVirtualServer.Instance.SetHomeSceneDTO(dto);
                return canSave;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestHomeSceneRepositoryがサーバへの書き込みに失敗しました");
                return false;
            }
        }
    }
}
