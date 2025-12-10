using System;
using Cysharp.Threading.Tasks;
using GiikutenApplication.MessageScene.Data;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : IWebSocketClient
{
    private WebSocket _ws;
    private readonly string _url = "ws://localhost/go/chat/ws"; // 固定 URL
    private readonly string _sessionId = SessionManager.SessionId;

    // 受信イベントを外部に公開
    public event Func<MessageInDTO, UniTask> OnMessageReceived;

    public WebSocketClient()
    {
        _ws = new WebSocket(_url);

        // Cookie でセッション送信
        _ws.SetCookie(new WebSocketSharp.Net.Cookie("session", _sessionId));

        _ws.OnMessage += async (sender, e) =>
        {
            try
            {
                var dto = JsonUtility.FromJson<MessageInDTO>(e.Data);
                if (OnMessageReceived != null)
                    await OnMessageReceived.Invoke(dto);
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON変換失敗: {ex}");
            }
        };

        _ws.OnError += (sender, e) =>
        {
            Debug.LogError($"WebSocket エラー: {e.Message}");
        };

        _ws.OnClose += (sender, e) =>
        {
            Debug.Log($"WebSocket クローズ: {e.Reason}");
        };
    }

    public UniTask ConnectAsync()
    {
        if (_ws.ReadyState == WebSocketState.Open)
            return UniTask.CompletedTask;

        var tcs = new UniTaskCompletionSource();
        _ws.OnOpen += (sender, e) => tcs.TrySetResult();
        _ws.ConnectAsync();
        return tcs.Task;
    }

    private async UniTask EnsureConnectedAsync()
    {
        if (_ws.ReadyState != WebSocketState.Open)
        {
            Debug.Log("WebSocket 接続待機中...");
            await ConnectAsync();
        }
    }

    public async UniTask SendMessageAsync(object dto)
    {
        await EnsureConnectedAsync();

        try
        {
            string json = JsonUtility.ToJson(dto);
            _ws.Send(json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"SendMessageAsync Exception: {ex}");
        }
    }

    public async UniTask<MessageInDTO> FetchMessageInDTO(Guid roomID)
    {
        var tcs = new UniTaskCompletionSource<MessageInDTO>();

        // 受信時に UniTaskCompletionSource に結果をセット
        async UniTask Handler(MessageInDTO dto)
        {
            tcs.TrySetResult(dto);
            OnMessageReceived -= Handler; // イベント解除
        }

        // Handler をイベントに登録
        OnMessageReceived += Handler;

        // サーバにリクエスト送信
        await SendMessageAsync(new { type = "subscribe", roomid = roomID });

        return await tcs.Task;
    }


    public async UniTask<bool> SaveMessageOutDTO(MessageOutDTO dto)
    {
        await SendMessageAsync(dto);
        return true; // ACK が無い場合は簡易的に成功扱い
    }

    public void Close()
    {
        _ws.Close();
    }
}
