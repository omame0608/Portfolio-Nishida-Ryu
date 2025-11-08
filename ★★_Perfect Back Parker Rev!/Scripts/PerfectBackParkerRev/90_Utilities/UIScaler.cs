using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PerfectBackParkerRev.Utilities
{
    /// <summary>
    /// マウスカーソルの重なりによって大きさを変更する
    /// </summary>
    public class UIScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //操作する要素
        private RectTransform _rectTransform;

        //設定項目
        [SerializeField, Header("UIの拡大率")] private float _zoomScale = 1.2f;
        [SerializeField, Header("アニメーションの時間")] private float _duration = 0.2f;
        [SerializeField, Header("TimeScaleを無視するか")] private bool _ignoreTimeScale = false;


        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        private void OnEnable()
        {
            _rectTransform.DOScale(new Vector3(1f, 1f, 1f), 0f).SetUpdate(_ignoreTimeScale);
        }


        /// <summary>
        /// カーソルが重なったらサイズを大きくする
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.DOScale(new Vector3(_zoomScale, _zoomScale, 1f), _duration).SetUpdate(_ignoreTimeScale);
        }


        /// <summary>
        /// カーソルが離れたらサイズを小さくする
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _rectTransform.DOScale(new Vector3(1f, 1f, 1f), _duration).SetUpdate(_ignoreTimeScale);
        }
    }
}