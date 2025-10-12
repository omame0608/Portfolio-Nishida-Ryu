using DG.Tweening;
using DG.Tweening.Core.Easing;
using ParkingGame.Audio;
using ParkingGame.GameSystems.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// エンディング画面View
    /// </summary>
    public class EndingView : MonoBehaviour, IEndingView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //View
        [SerializeField] private RectTransform _endingPanel;
        [SerializeField] private RectTransform _congratulation;


        public void ShowEndingView()
        {
            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _seManager.PlaySE(SE.Ending);
                    _endingPanel.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                })
                .Append(_endingPanel.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutElastic))
                .Append(_congratulation.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));
        }
    }
}