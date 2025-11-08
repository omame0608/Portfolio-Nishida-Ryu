using Alchemy.Inspector;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// クリアカウントビュー
    /// </summary>
    public class ClearCountView : MonoBehaviour, IClearCountView
    {
        [Header("操作対象のオブジェクト")]
        [SerializeField] private TMP_Text _clearCountText; //カウントテキスト
        [SerializeField] private Image _circle1; //サークル1周目
        [SerializeField] private Image _circle2; //サークル2周目
        [SerializeField] private Image _circle3; //サークル3周目

        //Audio
        [Inject] private SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Tweener _tweener1;
        private Tweener _tweener2;
        private Tweener _tweener3;

        //View操作可否
        private bool _canDisplayCircle;
        public bool CanDisplayCircle
        {
            get => _canDisplayCircle;
            set
            {
                //falseセット時にはCancelを実行して瞬時に非表示にする
                _canDisplayCircle = value;
                if (!_canDisplayCircle) Cancel();
            }
        }


        public void ShowCountOnce(int count)
        {
            if (!_canDisplayCircle) return;
            
            //カウントを更新してViewを表示
            _clearCountText.text = $"{count}";
            gameObject.SetActive(true);

            //アニメーションが残っていた場合キル
            _tweener1.Kill(true);
            _tweener2.Kill(true);
            _tweener3.Kill(true);

            //カウントに応じてアニメーションを再生
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
            //アニメーションが残っていた場合キル
            _tweener1.Kill(true);
            _tweener2.Kill(true);
            _tweener3.Kill(true);

            //Viewを非表示化
            gameObject.SetActive(false);
        }


        [Title("単体テスト用")]
        [Button] private void TestShowCountOnce3() => ShowCountOnce(3);
        [Button] private void TestShowCountOnce2() => ShowCountOnce(2);
        [Button] private void TestShowCountOnce1() => ShowCountOnce(1);
        [Button] private void TestCancel() => Cancel();
        [Button] private void TestToggleCanDisplayCircle() => CanDisplayCircle = !CanDisplayCircle;
    }
}