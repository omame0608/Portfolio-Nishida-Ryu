using System;
using Cysharp.Threading.Tasks;
using GiikutenApplication.MessageScene.Data;

public interface IWebSocketClient
{
    UniTask ConnectAsync();
    UniTask<MessageInDTO> FetchMessageInDTO(Guid roomID);
    UniTask<bool> SaveMessageOutDTO(MessageOutDTO dto);
    void Close();
    event Func<MessageInDTO, UniTask> OnMessageReceived;
}