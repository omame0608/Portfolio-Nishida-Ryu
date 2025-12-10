using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.Transition
{
    /// <summary>
    /// SettingsSceneへの遷移アニメーションを制御
    /// </summary>
    public class TransitionPanel : MonoBehaviour
    {
        //操作パーツ
        [SerializeField] private RectTransform _upPlate;
        [SerializeField] private RectTransform _downPlate;
        [SerializeField] private RectTransform _iconRect;
        [SerializeField] private Image _icon;

        //キャンセル用
        private Tween _tween;


        private void OnEnable()
        {
            //遷移時の破棄を禁止
            DontDestroyOnLoad(gameObject);

            //アニメーションが残っていたらキル
            _tween?.Kill();

            //アニメーション
            _tween = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    //初期設定
                    _upPlate.anchoredPosition = new Vector3(2000f, 557f, 0f);
                    _downPlate.anchoredPosition = new Vector3(-2000f, -557f, 0f);
                    var c = _icon.color;
                    c.a = 0f;
                    _icon.color = c;
                })
                .Append(_upPlate.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_downPlate.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_icon.DOFade(1f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_iconRect.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart))
                .AppendInterval(1f)
                .Append(_upPlate.DOAnchorPosX(-2000f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_downPlate.DOAnchorPosX(2000f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_icon.DOFade(0f, 0.5f).SetEase(Ease.OutQuart))
                .Join(_iconRect.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuart))
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}