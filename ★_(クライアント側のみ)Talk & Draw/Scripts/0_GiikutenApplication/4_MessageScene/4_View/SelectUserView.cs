using GiikutenApplication.Common;
using GiikutenApplication.MessageScene.Data;
using GiikutenApplication.MessageScene.Presentation.IView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.MessageScene.View
{
    /// <summary>
    /// Message画面の会話相手選択View
    /// </summary>
    public class SelectUserView : MonoBehaviour, ISelectUserView
    {
        //選択パネル配置場所
        [SerializeField] private RectTransform _content;
        [SerializeField] private RectTransform _userPanelPrefab;

        //変換用
        [Inject] private readonly OnlineImageGetter _imageGetter;
        [Inject] private readonly IObjectResolver _resolver;


        public async void SetSelectUserView(ChatInfoDTO[] dtoArray)
        {
            //contentのサイズ調整
            var height = _content.sizeDelta;
            height.y = 200 * dtoArray.Length;
            _content.sizeDelta = height;


            Image[] images = new Image[dtoArray.Length];
            for (int i = 0; i < dtoArray.Length; i++)
            {
                Debug.Log($"{dtoArray[i].RoomID}, {dtoArray[i].UserName}, {dtoArray[i].ImageURL}");

                //panel生成
                var panel = _resolver.Instantiate(_userPanelPrefab, _content);

                //panelの位置調整
                panel.anchoredPosition = new Vector3(0, -100-i*200, 0);

                //データ反映
                var view = panel.GetComponent<UserPanelView>();
                view.RoomID = dtoArray[i].RoomID;
                view.UserName.text = dtoArray[i].UserName;
                images[i] = view.Image;
            }

            //画像を取得し反映
            for (int i = 0; i < dtoArray.Length; i++)
            {
                images[i].sprite = await _imageGetter.GetImageWithURL(dtoArray[i].ImageURL);
            }
        }
    }
}