using Cysharp.Threading.Tasks;
using DG.Tweening;
using ParkingGame.Audio;
using ParkingGame.GameSystems.View;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// クリアまでのカウントを表示するView
    /// </summary>
    public class ClearCountView : MonoBehaviour, IClearCountView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //View
        [SerializeField] private TMP_Text _textClearCount;
        [SerializeField] private Image _circle1;
        [SerializeField] private Image _circle2;
        [SerializeField] private Image _circle3;

        //アニメーション
        private Tweener _tweener1;
        private Tweener _tweener2;
        private Tweener _tweener3;

        public bool CanDisplayCircle { get; set; } = true;


        public void ShowCountOnce(int count)
        {
            if (!CanDisplayCircle) return;
            Debug.Log($"<color=yellow>クリアカウント：{count}</color>");
            _textClearCount.text = $"{count}";
            gameObject.SetActive(true);

            //アニメーションが残っていた場合キル
            _tweener1.Kill(true);
            _tweener2.Kill(true);
            _tweener3.Kill(true);

            switch (count)
            {
                case 3:
                    _circle1.fillAmount = 0f;
                    _circle2.fillAmount = 0f;
                    _circle3.fillAmount = 0f;
                    _seManager.PlaySE(SE.Circle1);
                    _tweener1 = DOTween.To(() => _circle1.fillAmount, x => _circle1.fillAmount = x, 1f, 1f).SetEase(Ease.OutQuad);
                    break;
                case 2:
                    _circle1.fillAmount = 1f;
                    _circle2.fillAmount = 0f;
                    _circle3.fillAmount = 0f;
                    _seManager.PlaySE(SE.Circle2);
                    _tweener2 = DOTween.To(() => _circle2.fillAmount, x => _circle2.fillAmount = x, 1f, 1f).SetEase(Ease.OutQuad);
                    break;
                case 1:
                    _circle1.fillAmount = 1f;
                    _circle2.fillAmount = 1f;
                    _circle3.fillAmount = 0f;
                    _seManager.PlaySE(SE.Circle3);
                    _tweener3 = DOTween.To(() => _circle3.fillAmount, x => _circle3.fillAmount = x, 1f, 1f).SetEase(Ease.OutQuad);
                    break;
            }            
        }

        public void Cancel()
        {
            Debug.Log($"<color=yellow>クリアカウント停止</color>");
            gameObject.SetActive(false);
        }
    }
}