using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GiikutenApplication.Pop
{
    /// <summary>
    /// パック抽選結果表示のアニメーションを表示する
    /// </summary>
    public class PackOpenAnimationCanvas : MonoBehaviour
    {
        //演出中に表示するデータ
        public string UserName { get; set; } = "";
        public string JobName { get; set; } = "";
        public string Advice { get; set; } = "";

        //背景画像一覧
        [SerializeField] private List<Sprite> _sprites = new();


        //操作パーツ
        [SerializeField] private CanvasGroup _backGround;
        [SerializeField] private RectTransform _gift;
        [SerializeField] private RectTransform _giftTop;
        [SerializeField] private CanvasGroup _flash;
        [SerializeField] private Image _charaImage;
        [SerializeField] private Text _userName;
        [SerializeField] private Text _jobName;
        [SerializeField] private Text _advice;
        [SerializeField] private CanvasGroup _information;
        [SerializeField] private RectTransform _informationRect;

        //キャンセル用
        private Tween _tween;


        /// <summary>
        /// 演出を再生する
        /// </summary>
        public async void OnEnable()
        {
            //データ更新を待機
            await UniTask.Yield();
            Debug.Log($"{UserName}, {JobName}, {Advice}");

            //アニメーションが残っていたらキル
            _tween?.Kill();

            //アニメーション
            _tween = DOTween.Sequence()
                .Append(_backGround.DOFade(1f, 0.5f).SetEase(Ease.OutQuart))
                .Append(_gift.DOAnchorPosY(0f, 1f).SetEase(Ease.OutBounce))
                .AppendCallback(() => _giftTop.DORotate(new Vector3(0f, 0f, 120f), 2f).SetEase(Ease.InSine))
                .AppendInterval(0.5f)
                .Append(_flash.DOFade(1f, 1f).SetEase(Ease.InSine))
                .AppendCallback(() =>
                {
                    //画像用意
                    switch (JobName)
                    {
                        case "Swordsman": _charaImage.sprite = _sprites[0]; break;
                        case "Mage": _charaImage.sprite = _sprites[1]; break;
                        case "Knight": _charaImage.sprite = _sprites[2]; break;
                        case "Ninja": _charaImage.sprite = _sprites[3]; break;
                        case "Thief": _charaImage.sprite = _sprites[4]; break;
                        case "Archer": _charaImage.sprite = _sprites[5]; break;
                        case "Clown": _charaImage.sprite = _sprites[6]; break;
                        case "Berserker": _charaImage.sprite = _sprites[7]; break;
                        case "Bard": _charaImage.sprite = _sprites[8]; break;
                        case "Alchemist": _charaImage.sprite = _sprites[9]; break;
                        case "Priest": _charaImage.sprite = _sprites[10]; break;
                        default: _charaImage.sprite = null; break;
                    }

                    //画像表示
                    var c = _charaImage.color;
                    c.a = 1f;
                    _charaImage.color = c;

                    //テキスト変更
                    _userName.text = UserName;
                    _jobName.text = JobName;
                    _advice.text = Advice;
                })
                .AppendInterval(0.5f)
                .Append(_flash.DOFade(0f, 1f).SetEase(Ease.OutExpo))
                .Append(_information.DOFade(1f, 0.5f).SetEase(Ease.InSine))
                .Join(_informationRect.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutExpo))
                .AppendCallback(async () =>
                {
                    //左クリックで演出終了
                    await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
                    await UniTask.Yield();
                    Destroy(gameObject);
                });

        }
    }
}