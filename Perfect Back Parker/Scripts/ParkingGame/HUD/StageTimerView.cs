using DG.Tweening;
using DG.Tweening.Core.Easing;
using ParkingGame.Audio;
using ParkingGame.GameSystems.View;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// ステージの残り時間を示すタイマーView
    /// </summary>
    public class StageTimerView : MonoBehaviour, IStageTimerView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //View
        [SerializeField] private TMP_Text _textStageTimer;

        //キャンセル用
        private Sequence _sequence;

        public void ShowTimer()
        {
            gameObject.SetActive(true);

            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            var _rt = _textStageTimer.GetComponent<RectTransform>();
            //演出
            DOTween.Sequence()
                .Append(_rt.DOScale(new Vector3(1.3f,1.3f,1.3f),0.5f).SetEase(Ease.InOutQuint))
                .AppendInterval(2f)
                .Append(_rt.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.InOutQuint));
        }


        public void HideTimer()
        {
            gameObject.SetActive(false);
        }


        public void UpdateTimerView(int time)
        {
            //Viewを更新
            _textStageTimer.text = $"残り：{time}秒";

            if (time % 2 == 0) _seManager.PlaySE(SE.Timer1);
            else _seManager.PlaySE(SE.Timer2);

            if (time == 30 || time == 10)
            {
                _seManager.PlaySE(SE.Alert);
                _textStageTimer.GetComponent<RectTransform>().DOShakeAnchorPos(1f);
            }
        }
    }
}