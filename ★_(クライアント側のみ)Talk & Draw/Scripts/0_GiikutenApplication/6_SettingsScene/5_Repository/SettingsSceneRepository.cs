using Cysharp.Threading.Tasks;
using GiikutenApplication.SettingsScene.Data;
using GiikutenApplication.SettingsScene.Domain;
using GiikutenApplication.Common.Network;
using System;
using UnityEngine;

namespace GiikutenApplication.SettingsScene.Repository
{
    public class SettingsSceneRepository : ISettingsSneneRepository
    {
            private readonly string baseUrl = "http://localhost/java/setting/";
            private readonly HttpClient _httpClient = HttpClient.Instance;
            private readonly string _sessionId = SessionManager.SessionId;
        


        public async UniTask<SettingsInDTO> Fetch()
        {
            try
            {
                Debug.Log("sessionid"+ _sessionId);
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl + "read", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Fetch 失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"Fetchレスポンス: {json}");
                return JsonUtility.FromJson<SettingsInDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Fetch Exception: {ex}");
                return null;
            }
        }

        public async UniTask<bool> Save(SettingsInDTO dto)
        {
            try
            {
                string cookie = _sessionId;
                string json = JsonUtility.ToJson(dto);
                Debug.Log($"Save送信データ: {json}");

                bool success = await _httpClient.Post(baseUrl + "create", json, cookie);
                return success;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Save Exception: {ex}");
                return false;
            }
        }
    }
}
