using Alchemy.Inspector;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ウェーブ失敗のビュー
    /// </summary>
    public class FailedView : MonoBehaviour, IFailedView
    {
        [Header("操作対象のオブジェクト")]
        [SerializeField] private CanvasGroup _shadows; //背景の影
        [SerializeField] private RectTransform _failed; //Failedテキスト
        [SerializeField] private RectTransform _retry; //Retryテキスト
        [SerializeField] private TMP_Text _retryText; //Retryテキスト

        //Audio
        [Inject] private SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _sequence;


        public void ShowFailedResult()
        {
            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            //初期設定
            _shadows.alpha = 0f;
            _failed.DOAnchorPosY(350f, 0f).SetRelative();
            _failed.DORotate(Vector3.zero, 0f);
            _retry.DOAnchorPosX(-200f, 0f).SetRelative();
            _retryText.alpha = 0f;

            //Viewを表示
            gameObject.SetActive(true);

            //SE再生
            _seManager.PlaySE(SE.Crash);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                .Append(_failed.DOAnchorPosY(-350f, 1f).SetEase(Ease.OutBounce).SetRelative())
                .Join(_shadows.DOFade(1f, 0.3f))
                .Append(_failed.DORotate(new Vector3(0f, 0f, -15f), 1f).SetEase(Ease.OutBounce))
                .Join(DOTween.To(() => _retryText.alpha, a => _retryText.alpha = a, 1f, 1f))
                .Join(_retry.DOAnchorPosX(200f, 1f).SetEase(Ease.OutQuart).SetRelative())
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _sequence = null;
                });
        }


        public void HideFailedResult()
        {
            //アニメーションをキル
            _sequence?.Kill(true);

            //Viewを非表示にする
            gameObject.SetActive(false);
        }


        [Title("単体テスト用")]
        [Button] private void TestShowFailedResult() => ShowFailedResult();
        [Button] private void TestHideFailedResult() => HideFailedResult();
    }
}