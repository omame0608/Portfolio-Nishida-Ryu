using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PerfectBackParkerRev.Utilities
{
    /// <summary>
    /// クリックを検出できるボタンUI
    /// </summary>
    public class ClickableButton : MonoBehaviour, IPointerClickHandler
    {
        //ボタン購読用
        private readonly Subject<Unit> _onClickSubject = new Subject<Unit>();
        public IObservable<Unit> OnClickSubject => _onClickSubject;


        /// <summary>
        /// クリックを検出しイベントを発火する
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _onClickSubject.OnNext(Unit.Default);
        }
    }
}
