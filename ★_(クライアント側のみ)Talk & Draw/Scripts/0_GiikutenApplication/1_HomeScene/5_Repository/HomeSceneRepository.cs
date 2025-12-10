using Cysharp.Threading.Tasks;
using GiikutenApplication.HomeScene.Data;
using GiikutenApplication.HomeScene.Domain;
using GiikutenApplication.Common.Network;
using System;
using UnityEngine;

namespace GiikutenApplication.HomeScene.Repository
{
    /// <summary>
    /// HomeScene用：サーバとの通信を行う窓口
    /// </summary>
    public class HomeSceneRepository : IHomeSceneRepository
    {
        private readonly string baseUrl = "http://localhost/java/home";
        private readonly HttpClient _httpClient = HttpClient.Instance;
        private readonly string _sessionId = SessionManager.SessionId;


        public async UniTask<HomeSceneDTO> Fetch()
        {
            try
            {
                Debug.Log("sessionid: " + _sessionId);
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl, cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Fetch 失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"Fetchレスポンス: {json}");
                return JsonUtility.FromJson<HomeSceneDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Fetch Exception: {ex}");
                return null;
            }
        }

        public async UniTask<bool> Save(HomeSceneDTO dto)
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
