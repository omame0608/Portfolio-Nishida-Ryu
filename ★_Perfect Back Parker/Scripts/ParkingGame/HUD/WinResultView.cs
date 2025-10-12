using DG.Tweening;
using DG.Tweening.Core.Easing;
using ParkingGame.Audio;
using ParkingGame.GameSystems;
using ParkingGame.GameSystems.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// ステージクリア時のView
    /// </summary>
    public class WinResultView : MonoBehaviour, IWinResultView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //演出オブジェクト
        [Inject] private ICameraSystem _camera;
        [SerializeField] private RectTransform _clearUI;
        [SerializeField] private RectTransform _nextUI;
        private TMP_Text _nextUIText;


        //キャンセル用
        private Sequence _modelSequence;
        private Sequence _UISequence;


        public void ShowWinResult()
        {
            //アニメーションが残っていた場合キルする
            _modelSequence?.Kill(true);
            _UISequence?.Kill(true);

            //Viewを表示
            gameObject.SetActive(true);

            _seManager.PlaySE(SE.Clear);

            //演出シーケンス：カメラ
            _modelSequence = DOTween.Sequence()
                .Append(_camera.Transform.DORotate(new Vector3(-60, _camera.Transform.rotation.y, 0f), 0.5f))
                .Append(_camera.Transform.DORotate(new Vector3(0f,30f,0f), 1f)
                            .SetEase(Ease.Linear)
                            .SetRelative()
                            .SetLoops(-1, LoopType.Incremental));

            //演出シーケンス：UI
            _UISequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _clearUI.anchoredPosition = new Vector3(-1050f, 0f, 0f);
                    _nextUI.anchoredPosition = new Vector3(300f, -300f, 0f);
                    _nextUIText = _nextUI.GetComponent<TMP_Text>();
                    _nextUIText.alpha = 0f;
                })
                .Append(_clearUI.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .AppendInterval(1f)
                .Append(DOTween.To(() => _nextUIText.alpha, a => _nextUIText.alpha = a, 1f, 1f))
                .Join(_nextUI.DOAnchorPosX(550f, 1f));
        }


        public void HideWinResult(bool isFinal = false)
        {
            //Viewを非表示にする
            gameObject.SetActive(false);

            if (isFinal) return;

            //アニメーションをキル
            _modelSequence.Kill(true);
            _UISequence.Kill(true);
        }
    }
}