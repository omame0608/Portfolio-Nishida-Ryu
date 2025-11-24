using Alchemy.Inspector;
using DG.Tweening;
using UnityEngine;

namespace SupernovaTrolley.HUDs
{
    /// <summary>
    /// オープニングビュー
    /// </summary>
    public class OpeningView : MonoBehaviour
    {
        [Title("操作対象のオブジェクト")]
        [SerializeField] private GameObject _interactionBlock;
        [SerializeField] private CanvasGroup _openingCanvas;

        //キャンセル用
        private Sequence _sequence;


        /// <summary>
        /// オープニングキャンバスを非表示にする
        /// </summary>
        public async void HideOpeningView()
        {
            //アニメーションをキャンセル
            _sequence?.Kill(true);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    //操作ブロックを有効化
                    _interactionBlock.SetActive(true);
                })
                //Viewをフェードアウト
                .Append(_openingCanvas.DOFade(0f, 0.5f).SetEase(Ease.InQuart))
                //完了時にシーケンスをクリア
                .OnComplete(() =>
                {
                    _sequence = null;
                })
                .SetLink(gameObject);
            await _sequence.AsyncWaitForCompletion();

            //非表示にする
            gameObject.SetActive(false);
        }


        [Title("デバッグ用")]
        [Button]
        private void TestHideOpeningView()
        {
            if (!Application.isPlaying) return;
            HideOpeningView();
        }
    }
}