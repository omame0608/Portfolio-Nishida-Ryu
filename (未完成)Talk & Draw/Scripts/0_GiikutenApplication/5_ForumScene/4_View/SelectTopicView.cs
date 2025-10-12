using GiikutenApplication.Common;
using GiikutenApplication.ForumScene.Data;
using GiikutenApplication.ForumScene.Presentation.IView;
using GiikutenApplication.MessageScene.View;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace GiikutenApplication.ForumScene.View
{
    /// <summary>
    /// Forum画面の会話相手選択View
    /// </summary>
    public class SelectTopicView : MonoBehaviour, ISelectTopicView
    {
        //選択パネル配置場所
        [SerializeField] private RectTransform _content;
        [SerializeField] private RectTransform _userPanelPrefab;

        //変換用
        [Inject] private readonly OnlineImageGetter _imageGetter;
        [Inject] private readonly IObjectResolver _resolver;


        public async void SetSelectTopicView(ForumDTO[] dtoArray)
        {
            //contentのサイズ調整
            var height = _content.sizeDelta;
            height.y = 200 * dtoArray.Length;
            _content.sizeDelta = height;


            Image[] images = new Image[dtoArray.Length];
            for (int i = 0; i < dtoArray.Length; i++)
            {
                Debug.Log($"{dtoArray[i].RoomID}, {dtoArray[i].UserName}, {dtoArray[i].ImageURL}, {dtoArray[i].Title}");

                //panel生成
                var panel = _resolver.Instantiate(_userPanelPrefab, _content);

                //panelの位置調整
                panel.anchoredPosition = new Vector3(0, -100 - i * 200, 0);

                //データ反映
                var view = panel.GetComponent<TopicPanelView>();
                view.RoomID = dtoArray[i].RoomID;
                view.Title.text = dtoArray[i].Title;
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