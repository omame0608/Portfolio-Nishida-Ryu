using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UtilView;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.ForumScene.View
{
    /// <summary>
    /// TopicPanel制御用
    /// </summary>
    public class TopicPanelView : MonoBehaviour
    {
        //保持するデータ
        private Guid _roomID;
        [SerializeField] private Text _title;
        [SerializeField] private Image _image;

        public Guid RoomID { get => _roomID; set { _roomID = value; } }
        public Text Title { get => _title; set { _title = value; } }
        public Image Image { get => _image; set { _image = value; } }

        //ChatCanvas
        [SerializeField] private ChatCanvasViewForForum _chatCanvas;

        [Inject] private readonly IObjectResolver _resolver;


        public void Awake()
        {
            var button = GetComponent<ClickableButton>();
            button.OnClickSubject
                .Subscribe(_ =>
                {
                    var newCanvas = _resolver.Instantiate(_chatCanvas);
                    newCanvas.SetChatCanvas(_roomID, _title.text);
                })
                .AddTo(this);
        }
    }
}