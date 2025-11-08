using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace PerfectBackParkerRev.Directions.StageSelects
{
    /// <summary>
    /// 演出用：ステージセレクトシーンでプレイヤーを動かすクラス
    /// </summary>
    public class StageSelectPlayerMover : MonoBehaviour
    {
        //操作対象のオブジェクト(タイヤView)
        [SerializeField] private Transform _wheelFLView; //タイヤ左前
        [SerializeField] private Transform _wheelFRView; //タイヤ右前
        [SerializeField] private Transform _wheelBLView; //タイヤ左後ろ
        [SerializeField] private Transform _wheelBRView; //タイヤ右後ろ

        //演出管理用
        private Sequence _wheelSequence; //タイヤ演出用シーケンス

        //演出調整用
        [SerializeField] private float _wheelMoveRotation = 360f; //移動時のタイヤの回転量
        [SerializeField] private float _wheelInSceneRotation = 720f; //シーン入り時のタイヤの回転量


        /// <summary>
        /// ステージ移動ジのタイヤの演出を再生する
        /// </summary>
        /// <param name="isMoveNext">次のステージへの移動か</param>
        /// <param name="duration">再生時間</param>
        public void PlayMoveDirection(bool isMoveNext, float duration)
        {
            //アニメーションが残っていたらキル
            _wheelSequence?.Kill(true);

            //回転方向を判定
            var rotate = 0f;
            if (isMoveNext) rotate = -_wheelMoveRotation;
            else rotate = _wheelMoveRotation;

            //演出シーケンス
            _wheelSequence = DOTween.Sequence()
                //タイヤを回転させる
                .Join(_wheelFLView.DORotate(new Vector3(rotate, 0f, 0f), duration).SetEase(Ease.OutQuart).SetRelative())
                .Join(_wheelFRView.DORotate(new Vector3(rotate, 0f, 0f), duration).SetEase(Ease.OutQuart).SetRelative())
                .Join(_wheelBLView.DORotate(new Vector3(rotate, 0f, 0f), duration).SetEase(Ease.OutQuart).SetRelative())
                .Join(_wheelBRView.DORotate(new Vector3(rotate, 0f, 0f), duration).SetEase(Ease.OutQuart).SetRelative())
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _wheelSequence = null;
                });
        }


        /// <summary>
        /// シーンに入ったときの演出を再生する
        /// </summary>
        /// <param name="duration">再生時間</param>
        public void PlayInSceneDirection(float duration)
        {
            //アニメーションが残っていたらキル
            _wheelSequence?.Kill(true);

            //演出シーケンス
            _wheelSequence = DOTween.Sequence()
                //タイヤを回転させる
                .Join(_wheelFLView.DORotate(new Vector3(-_wheelInSceneRotation, 0f, 0f), duration).SetEase(Ease.InOutBack).SetRelative())
                .Join(_wheelFRView.DORotate(new Vector3(-_wheelInSceneRotation, 0f, 0f), duration).SetEase(Ease.InOutBack).SetRelative())
                .Join(_wheelBLView.DORotate(new Vector3(-_wheelInSceneRotation, 0f, 0f), duration).SetEase(Ease.InOutBack).SetRelative())
                .Join(_wheelBRView.DORotate(new Vector3(-_wheelInSceneRotation, 0f, 0f), duration).SetEase(Ease.InOutBack).SetRelative())
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _wheelSequence = null;
                });
        }
    }
}