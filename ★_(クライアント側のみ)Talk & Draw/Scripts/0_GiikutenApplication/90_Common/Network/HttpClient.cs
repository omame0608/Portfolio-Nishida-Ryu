using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Text;

namespace GiikutenApplication.Common.Network
{
    public class HttpClient
    {
        // シングルトン
        public static HttpClient Instance { get; } = new HttpClient();

        // private コンストラクタで外部からのインスタンス化を禁止
        private HttpClient() { }

        /// <summary>
        /// GET リクエスト（Cookie をリクエスト単位で渡せる）
        /// </summary>
        /// <param name="url">リクエスト先 URL</param>
        /// <param name="cookie">JSESSIONID などの Cookie</param>
        /// <returns>レスポンス文字列</returns>
        public async UniTask<string> Get(string url, string cookie = null)
        {
            using var request = UnityWebRequest.Get(url);

            if (!string.IsNullOrEmpty(cookie))
            {
                request.SetRequestHeader("Cookie", cookie);
                Debug.Log($"HttpClient GET: Cookie={cookie}");
            }

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"HttpClient GET failed: {request.error}");
                return null;
            }

            return request.downloadHandler.text;
        }

        /// <summary>
        /// POST リクエスト（Cookie をリクエスト単位で渡せる）
        /// </summary>
        /// <param name="url">リクエスト先 URL</param>
        /// <param name="json">送信 JSON</param>
        /// <param name="cookie">JSESSIONID などの Cookie</param>
        /// <returns>成功したかどうか</returns>
        public async UniTask<bool> Post(string url, string json, string cookie = null)
        {
            using var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(cookie))
            {
                request.SetRequestHeader("Cookie", cookie);
                Debug.Log($"HttpClient POST: Cookie={cookie}");
            }

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"HttpClient POST failed: {request.error}");
                return false;
            }

            return true;
        }
    }
}
