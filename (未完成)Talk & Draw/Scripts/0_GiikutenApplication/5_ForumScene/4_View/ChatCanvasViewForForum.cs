using GiikutenApplication.Common;
using GiikutenApplication.ForumScene.Domain;
using GiikutenApplication.MessageScene.Data;
using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GiikutenApplication.ForumScene.View
{
    /// <summary>
    /// ChatCanvas制御用
    /// </summary>
    public class ChatCanvasViewForForum : MonoBehaviour
    {
        //最新のインスタンスのみ存在可能
        private static ChatCanvasViewForForum _currentCanvas;

        //操作するパーツ
        private Guid _roomID; //ルームID
        [SerializeField] private Text _title;

        //操作用
        [Inject] private readonly IForumSceneRepository _repository;
        [Inject] private readonly OnlineImageGetter _imageGetter;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GameObject _myChatNode;
        [SerializeField] private GameObject _otherChatNode;
        [SerializeField] private TMP_InputField _input;


        private void Awake()
        {
            //最新のインスタンスのみ存在可能
            if (_currentCanvas != null)
            {
                Destroy(_currentCanvas.gameObject);
            }
            _currentCanvas = this;

            //入力完了を監視
            _input.onEndEdit.AsObservable()
                .Subscribe(async _ =>
                {
                    var myMessage = _input.text;
                    if (myMessage == "") return;
                    await _repository.SaveMessageOutDTOForForum(new MessageOutDTO(_roomID, myMessage));
                    _input.SetTextWithoutNotify("");
                    _input.DeactivateInputField();
                })
                .AddTo(this);

            //テスト用：サーバから新しくメッセージを受信したときの処理
            //現在はSpaceキーで処理を開始する
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(async _ =>
                {
                    //サーバからメッセージをフェッチ
                    var message = await _repository.FetchMessageInDTOForForum(_roomID);
                    if (message == null)
                    {
                        Debug.Log($"データの取得に失敗、または会話履歴なし");
                    }

                    //メッセージを表示
                    //自分のメッセージ
                    if (message.IsPerson)
                    {
                        var panel = Instantiate(_myChatNode, _content);
                        panel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Message;
                        var sprite = await _imageGetter.GetImageWithURL(message.ImageURL);
                        panel.transform.GetChild(1).GetComponent<Image>().sprite = sprite;
                    }
                    //相手のメッセージ
                    else
                    {
                        var panel = Instantiate(_otherChatNode, _content);
                        panel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = message.Message;
                        var sprite = await _imageGetter.GetImageWithURL(message.ImageURL);
                        panel.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                    }

                    _scrollRect.verticalNormalizedPosition = 0f;
                })
                .AddTo(this);
        }


        /// <summary>
        /// ChatCanvasにデータを表示する
        /// </summary>
        public async void SetChatCanvas(Guid roomID, string userName)
        {
            _roomID = roomID;
            _title.text = userName;

            //会話履歴全フェッチ
            var messageArray = await _repository.FetchAllMessageInDTOForForum(roomID);
            if (messageArray == null)
            {
                Debug.Log($"データの取得に失敗、または会話履歴なし");
            }

            //会話表示
            List<Image> icons = new List<Image>();
            for (int i = 0; i < messageArray.Length; i++)
            {
                //自分のメッセージ
                if (messageArray[i].IsPerson)
                {
                    var panel = Instantiate(_myChatNode, _content);
                    panel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = messageArray[i].Message;
                    icons.Add(panel.transform.GetChild(1).GetComponent<Image>());
                }
                //相手のメッセージ
                else
                {
                    var panel = Instantiate(_otherChatNode, _content);
                    panel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = messageArray[i].Message;
                    icons.Add(panel.transform.GetChild(0).GetComponent<Image>());
                }
                _scrollRect.verticalNormalizedPosition = 0f;
            }

            //画像読み込み
            for (int i = icons.Count - 1; i >= 0; i--)
            {
                var sprite = await _imageGetter.GetImageWithURL(messageArray[i].ImageURL);
                icons[i].sprite = sprite;
            }
        }
    }
}