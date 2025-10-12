using Cysharp.Threading.Tasks;
using GiikutenApplication.SettingsScene.Data;
using GiikutenApplication.SettingsScene.Domain;
using System;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：テストサーバとの通信を行う窓口
    /// あとから本番サーバとの通信を行う窓口に置き換える
    /// </summary>
    public class TestSettingsSceneRepository : ISettingsSneneRepository
    {
        //通信するサーバ
        //private TestVirtualServer _server = TestVirtualServer.Instance;

        public async UniTask<SettingsInDTO> Fetch()
        {
            try
            {
                var dto = await TestVirtualServer.Instance.GetSettingsInDTO();
                return dto;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestSettingsSceneRepositoryがサーバからの読み込みに失敗しました");
                return null;
            }
        }


        public async UniTask<bool> Save(SettingsInDTO dto)
        {
            try
            {
                var canSave = await TestVirtualServer.Instance.SetSettingsInDTO(dto);
                return canSave;
            }
            catch (Exception ex)
            {
                Debug.LogError($"TestSettingsSceneRepositoryがサーバへの書き込みに失敗しました");
                return false;
            }
        }
    }
}
