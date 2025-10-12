using DG.Tweening;
using DG.Tweening.Core.Easing;
using ParkingGame.Audio;
using ParkingGame.GameSystems.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// ステージ失敗時のView
    /// </summary>
    public class LoseResultView : MonoBehaviour, ILoseResultView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //演出オブジェクト
        [SerializeField] private Image _topFade;
        [SerializeField] private Image _bottomFade;
        [SerializeField] private RectTransform _failedUI;
        [SerializeField] private RectTransform _nextUI;
        private TMP_Text _nextUIText;

        //キャンセル用
        private Sequence _UISequence;
        

        public void ShowLoseResult()
        {
            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス：UI
            _UISequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _seManager.PlaySE(SE.Crash);
                    _failedUI.anchoredPosition = new Vector3(-500f, 650f, 0f);
                    _nextUI.anchoredPosition = new Vector3(300f, -300f, 0f);
                    var color = _topFade.color;
                    color.a = 0f;
                    _topFade.color = color;
                    color = _bottomFade.color;
                    color.a = 0f;
                    _bottomFade.color = color;
                    _nextUIText = _nextUI.GetComponent<TMP_Text>();
                    _nextUIText.alpha = 0f;
                })
                .Append(_failedUI.DOAnchorPosY(300f, 1f).SetEase(Ease.OutBounce))
                .Join(_topFade.DOFade(1f, 1f))
                .Join(_bottomFade.DOFade(1f, 1f))
                .AppendInterval(1f)
                .Append(DOTween.To(() => _nextUIText.alpha, a => _nextUIText.alpha = a, 1f, 1f))
                .Join(_nextUI.DOAnchorPosX(550f, 1f));
        }


        public void HideLoseResult()
        {
            //Viewを非表示にする
            gameObject.SetActive(false);

            //アニメーションをキル
            _UISequence.Kill(true);
        }
    }
}