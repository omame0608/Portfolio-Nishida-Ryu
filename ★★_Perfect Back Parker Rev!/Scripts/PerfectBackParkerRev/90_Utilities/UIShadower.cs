using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PerfectBackParkerRev.Utilities
{
    /// <summary>
    /// クリック時にUIを暗くする
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIShadower : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //操作する要素
        private CanvasGroup _canvasGroup;

        //設定項目
        [SerializeField, Header("UIの透明度")] private float _darkAlpha = 0.7f;
        [SerializeField, Header("アニメーションの時間")] private float _duration = 0.1f;


        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        /// <summary>
        /// クリックされたらUIを暗くする
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _canvasGroup.DOFade(_darkAlpha, _duration);
        }


        /// <summary>
        /// 離されたらUIの明るさを元に戻す
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            _canvasGroup.DOFade(1, _duration);
        }
    }
}