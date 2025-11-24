using Alchemy.Inspector;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utilities;

namespace SupernovaTrolley.HUDs
{
    /// <summary>
    /// エンディングビュー
    /// </summary>
    public class EndingView : MonoBehaviour
    {
        [Title("操作対象のオブジェクト")]
        [SerializeField] private RectTransform _scoreRect;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private RectTransform _resultPanelRect;
        [SerializeField] private CanvasGroup _resultPanelCanvas;
        [SerializeField] private CanvasGroup _restartButtonCanvas;
        [SerializeField] private GameObject _interactionBlock;

        //キャンセル用
        private Sequence _sequence;


        /// <summary>
        /// エンディングビューを表示する
        /// </summary>
        /// <param name="score">スコア</param>
        public void ShowEndingView(int score)
        {
            //アニメーションをキャンセル
            _sequence?.Kill(true);

            //初期化
            _scoreText.text = 0.ToString();
            var resultPanelDelta = new Vector2(0f, -70f);
            _resultPanelRect.anchoredPosition -= resultPanelDelta;
            _resultPanelCanvas.alpha = 0f;
            _restartButtonCanvas.alpha = 0f;

            //表示
            gameObject.SetActive(true);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //結果パネルをフェードイン
                .Append(_resultPanelRect.DOAnchorPos(resultPanelDelta, 1f).SetRelative().SetEase(Ease.OutQuart))
                .Join(_resultPanelCanvas.DOFade(1f, 1f).SetEase(Ease.OutQuart))
                .AppendInterval(0.2f)
                //スコアをカウントアップ、終了後演出
                .Append(DOTween.To(() => 0, x => _scoreText.text = x.ToString(), score, 2f).SetEase(Ease.OutQuart))
                .Insert(3f, _scoreRect.DOPunchScale(Vector3.one * 0.2f, 0.5f).SetEase(Ease.OutQuart))
                //リスタートボタンをフェードイン
                .Append(_restartButtonCanvas.DOFade(1f, 0.5f).SetEase(Ease.OutQuart))
                //操作ブロックを無効化
                .AppendCallback(() =>
                {
                    _interactionBlock.SetActive(false);
                })
                //完了時にシーケンスをクリア
                .OnComplete(() =>
                {
                    _sequence = null;
                })
                .SetLink(gameObject);
        }


        /// <summary>
        /// エンディングビューへの操作を再度可能にする
        /// </summary>
        public void BlockEndingView()
        {
            _interactionBlock.SetActive(true);
        }


        [Title("デバッグ用")]
        [Button]
        private void TestShowEndingView()
        {
            if (!Application.isPlaying) return;
            ShowEndingView(77777);
        }
    }
}