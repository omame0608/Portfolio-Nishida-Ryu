using DG.Tweening;
using UnityEngine;

namespace PerfectBackParkerRev.Directions.StageSelects
{
    /// <summary>
    /// 演出用：ミニチュアの動きを制御する
    /// </summary>
    public class StageSelectMiniatureMover : MonoBehaviour
    {
        //キャンセル用
        private Sequence _playSequence; //上下移動ループ
        private Tweener _finishTweener; //初期位置へ移動


        /// <summary>
        /// 上下移動ループを開始する
        /// </summary>
        public void PlayAnimation()
        {
            //アニメーションが残っていたらキル
            _playSequence.Kill(true);
            _finishTweener.Kill(true);

            //演出
            _playSequence = DOTween.Sequence()
                //上下移動ループ
                .Append(transform.DOMoveY(0.2f, 1.5f).SetEase(Ease.OutCubic).SetRelative())
                .Append(transform.DOMoveY(-0.4f, 3f).SetEase(Ease.InOutCubic).SetRelative())
                .Append(transform.DOMoveY(0.2f, 1.5f).SetEase(Ease.InCubic).SetRelative())
                .SetLoops(-1);
        }


        /// <summary>
        /// 上下移動ループを停止して初期位置に戻る
        /// </summary>
        public void FinishAnimation()
        {
            //アニメーションが残っていたらキル
            _playSequence.Kill(true);
            _finishTweener.Kill(true);

            //演出
            _finishTweener = transform.DOMoveY(0.4f, 0.1f).SetEase(Ease.OutCubic);
        }
    }
}