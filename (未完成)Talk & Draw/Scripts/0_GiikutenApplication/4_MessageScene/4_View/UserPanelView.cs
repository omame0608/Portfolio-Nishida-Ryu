using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UtilView;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.MessageScene.View
{
    /// <summary>
    /// UserPanel制御用
    /// </summary>
    public class UserPanelView : MonoBehaviour
    {
        //保持するデータ
        private Guid _roomID;
        [SerializeField] private Text _userName;
        [SerializeField] private Image _image;

        public Guid RoomID { get => _roomID; set { _roomID = value; } }
        public Text UserName { get => _userName; set { _userName = value; } }
        public Image Image { get => _image; set { _image = value; } }

        //ChatCanvas
        [SerializeField] private ChatCanvasView _chatCanvas;

        [Inject] private readonly IObjectResolver _resolver;


        public void Awake()
        {
            var button = GetComponent<ClickableButton>();
            button.OnClickSubject
                .Subscribe(_ => 
                {
                    var newCanvas = _resolver.Instantiate(_chatCanvas);
                    newCanvas.SetChatCanvas(_roomID, _userName.text);
                })
                .AddTo(this);
        }
    }
}