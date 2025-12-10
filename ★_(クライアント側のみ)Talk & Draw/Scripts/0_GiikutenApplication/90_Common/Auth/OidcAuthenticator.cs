using System;
using System.Net;
using System.Text;
using UnityEngine; // UnityEngine.Debugを使うために追加

namespace GiikutenApplication.Common.Auth
{
    public class OidcAuthenticator
    {
        private HttpListener listener;
        private int port;

        public string SessionId { get; private set; }

        public OidcAuthenticator(int callbackPort = 5022)
        {
            port = callbackPort;
        }

        public void StartLoginFlow()
        {
            StartServer();
            OpenBrowser();
        }

        private void OpenBrowser()
        {
            string url = $"http://localhost/java/login";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void StartServer()
        {
            listener = new HttpListener();
            listener.Prefixes.Add($"http://+:{port}/callback/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
            Debug.Log($"OidcAuthenticator: Listening on http://localhost:{port}/callback/");
        }

        private void OnRequest(IAsyncResult result)
        {
            var context = listener.EndGetContext(result);

            // 次のリクエストも受け取れるように
            listener.BeginGetContext(OnRequest, null);

            // GET クエリから sessionId を取得
            string sessionId = context.Request.QueryString["sessionId"];
            SessionId = sessionId;
            SessionManager.SessionId = SessionId;
            

            Debug.Log($"OidcAuthenticator: Received sessionId: {sessionId}");

            // レスポンス送信
            string responseString = "<html><body>Login successful! You can close this window.</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();

            // サーバー停止
            listener.Stop();
            listener.Close();
        }
    }

}
