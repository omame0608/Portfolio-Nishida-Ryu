using Cysharp.Threading.Tasks;
using GiikutenApplication.PackScene.Data;
using GiikutenApplication.PackScene.Domain;
using GiikutenApplication.Common.Network;
using System;
using UnityEngine;

namespace GiikutenApplication.PackScene.Repository
{
    /// <summary>
    /// PackScene用：サーバとの通信を行う窓口
    /// </summary>
    public class PackSceneRepository : IPackSceneRepository
    {
        private readonly string baseUrl = "http://localhost/java/pack/";
        private readonly HttpClient _httpClient = HttpClient.Instance;
        private readonly string _sessionId = SessionManager.SessionId;


        public async UniTask<PackScreenDTO> Fetch()
        {
            try
            {
                Debug.Log("sessionid: " + _sessionId);
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl + "scene", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("Fetch失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"Fetchレスポンス: {json}");
                return JsonUtility.FromJson<PackScreenDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Fetch Exception: {ex}");
                return null;
            }
        }

        public async UniTask<bool> Save(PackScreenDTO dto)
        {
            try
            {
                string cookie = _sessionId;
                string json = JsonUtility.ToJson(dto);
                Debug.Log($"Save送信データ: {json}");

                bool success = await _httpClient.Post(baseUrl + "save", json, cookie);
                return success;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Save Exception: {ex}");
                return false;
            }
        }

        public async UniTask<RandomPackDTO> FetchRandomPack()
        {
            try
            {
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl + "randampack", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("FetchRandomPack失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"FetchRandomPackレスポンス: {json}");
                return JsonUtility.FromJson<RandomPackDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FetchRandomPack Exception: {ex}");
                return null;
            }
        }

        public async UniTask<RecommendPackDTO> FetchRecommendPack()
        {
            try
            {
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl + "recommnedampack", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("FetchRecommendPack失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"FetchRecommendPackレスポンス: {json}");
                return JsonUtility.FromJson<RecommendPackDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FetchRecommendPack Exception: {ex}");
                return null;
            }
        }
    }
}
