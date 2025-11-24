using Alchemy.Inspector;
using DG.Tweening;
using SupernovaTrolley.GameCores.Statuses;
using TMPro;
using UniRx;
using UnityEngine;
using Utilities;
using VContainer;

namespace SupernovaTrolley.HUDs
{
    /// <summary>
    /// ステータスビュー
    /// </summary>
    public class StatusView : MonoBehaviour
    {
        //システム
        [Inject] private TimeStatus _timeStatus;

        [Title("操作対象のオブジェクト")]
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private RectTransform _timerRect;
        [SerializeField] private CanvasGroup _timerCanvas;

        //キャンセル用
        private Sequence _sequence;


        private void Start()
        {
            //システムのモデルを監視
            _timeStatus.RemainingTime
                .Subscribe(remainingTime =>
                {
                    _timerText.text = remainingTime.ToString();
                })
                .AddTo(this);
        }


        /// <summary>
        /// ステータスビューを表示する
        /// </summary>
        public void ShowStatusView()
        {
            //アニメーションをキャンセル
            _sequence?.Kill(true);

            //初期化
            _timerText.text = 60.ToString();
            var timerDelta = new Vector2(0f, -50f);
            _timerRect.anchoredPosition -= timerDelta;
            _timerCanvas.alpha = 0f;

            //表示
            _timerRect.gameObject.SetActive(true);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //タイマーをフェードイン
                .Append(_timerRect.DOAnchorPos(timerDelta, 1f).SetRelative().SetEase(Ease.OutQuart))
                .Join(_timerCanvas.DOFade(1f, 1f).SetEase(Ease.OutQuart))
                //完了時にシーケンスをクリア
                .OnComplete(() =>
                {
                    _sequence = null;
                })
                .SetLink(gameObject);
        }


        /// <summary>
        /// ステータスビューを非表示にする
        /// </summary>
        public void HideStatusView()
        {
            //アニメーションをキャンセル
            _sequence?.Kill(true);

            //初期化
            var timerDelta = new Vector2(0f, 50f);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //タイマーをフェードアウト
                .Append(_timerRect.DOAnchorPos(timerDelta, 1f).SetRelative().SetEase(Ease.InQuart))
                .Join(_timerCanvas.DOFade(0f, 1f).SetEase(Ease.InQuart))
                //完了時にシーケンスをクリア
                .OnComplete(() =>
                {
                    //非表示にする
                    _timerRect.gameObject.SetActive(false);
                    _sequence = null;
                })
                .SetLink(gameObject);
        }


        [Title("デバッグ用")]
        [Button]
        private void TestShowStatusView()
        {
            if (!Application.isPlaying) return;
            ShowStatusView();
        }
        [Button]
        private void TestHideStatusView()
        {
            if (!Application.isPlaying) return;
            HideStatusView();
        }
    }
}