using Cysharp.Threading.Tasks;
using GiikutenApplication.MessageScene.Data;
using GiikutenApplication.MessageScene.Domain;
using GiikutenApplication.Common.Network;
using System;
using UnityEngine;

namespace GiikutenApplication.MessageScene.Repository
{
    /// <summary>
    /// MessageScene用：サーバとの通信を行う窓口
    /// </summary>
    public class MessageSceneRepository : IMessageSceneRepository
    {
        private readonly string baseUrl = "http://localhost/java/conversation/";
        private readonly HttpClient _httpClient = HttpClient.Instance;
        private readonly string _sessionId = SessionManager.SessionId;

        public async UniTask<ChatInfoDTO[]> FetchChatInfo()
        {
            try
            {
                string cookie = _sessionId;
                string json = await _httpClient.Get(baseUrl + "select", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("FetchChatInfo失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"FetchChatInfoレスポンス: {json}");
                return JsonHelper.FromJson<ChatInfoDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FetchChatInfo Exception: {ex}");
                return null;
            }
        }

        public async UniTask<MessageInDTO[]> FetchAllMessageInDTO(Guid roomID)
        {
            try
            {
                string cookie = _sessionId;
                string json = await _httpClient.Get($"{baseUrl}get/{roomID}", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("FetchAllMessageInDTO失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"FetchAllMessageInDTOレスポンス: {json}");
                return JsonHelper.FromJson<MessageInDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FetchAllMessageInDTO Exception: {ex}");
                return null;
            }
        }

        public async UniTask<MessageInDTO> FetchMessageInDTO(Guid roomID)
        {
            try
            {
                string cookie = _sessionId;
                string json = await _httpClient.Get($"{baseUrl}messagein/{roomID}", cookie);

                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogError("FetchMessageInDTO失敗: レスポンスが空");
                    return null;
                }

                Debug.Log($"FetchMessageInDTOレスポンス: {json}");
                return JsonUtility.FromJson<MessageInDTO>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"FetchMessageInDTO Exception: {ex}");
                return null;
            }
        }

        public async UniTask<bool> SaveMessageOutDTO(MessageOutDTO dto)
        {
            try
            {
                string cookie = _sessionId;
                string json = JsonUtility.ToJson(dto);
                Debug.Log($"SaveMessageOutDTO送信データ: {json}");

                bool success = await _httpClient.Post(baseUrl + "messageout", json, cookie);
                return success;
            }
            catch (Exception ex)
            {
                Debug.LogError($"SaveMessageOutDTO Exception: {ex}");
                return false;
            }
        }
    }

    /// <summary>
    /// JsonHelper: UnityのJsonUtilityで配列ルートJSONを安全に読み込むためのヘルパー
    /// </summary>
    public static class JsonHelper
    {
        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

        public static T[] FromJson<T>(string json)
        {
            // 配列をラッパーで包む
            json = "{\"Items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
    }
}
