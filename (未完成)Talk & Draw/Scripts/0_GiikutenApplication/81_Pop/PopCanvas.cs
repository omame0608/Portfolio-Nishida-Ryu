using DG.Tweening;
using UnityEngine;

namespace GiikutenApplication.Pop
{
    /// <summary>
    /// 一定時間インタラクション受付を停止してポップを表示する
    /// </summary>
    public class PopCanvas : MonoBehaviour
    {
        //操作パーツ
        [SerializeField] private CanvasGroup _canvasGroup;

        //キャンセル用
        private Tween _tween;


        private void OnEnable()
        {
            //アニメーションが残っていたらキル
            _tween?.Kill();

            var rect = _canvasGroup.GetComponent<RectTransform>();

            //アニメーション
            _tween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    //初期設定
                    _canvasGroup.alpha = 0f;
                    rect.anchoredPosition = new Vector3(0f, -100f, 0f);
                })
                .Append(rect.DOAnchorPosY(0f, 0.3f).SetEase(Ease.InOutElastic))
                .Join(_canvasGroup.DOFade(1f, 0.3f))
                .AppendInterval(2f)
                .Append(rect.DOAnchorPosY(100f, 0.3f).SetEase(Ease.InOutElastic))
                .Join(_canvasGroup.DOFade(0f, 0.3f))
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}