using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.Transition
{
    /// <summary>
    /// HomeSceneへの遷移アニメーションを制御
    /// </summary>
    public class TransitionPanelToHome : MonoBehaviour
    {
        //操作パーツ
        [SerializeField] private Image _fade;

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
                    var c = _fade.color;
                    c.a = 0f;
                    _fade.color = c;
                })
                .Append(_fade.DOFade(1f, 0.1f).SetEase(Ease.OutQuart))
                .AppendInterval(1f)
                .Append(_fade.DOFade(0f, 0.2f).SetEase(Ease.OutQuart))
                .AppendCallback(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}