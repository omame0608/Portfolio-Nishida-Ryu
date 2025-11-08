using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.OpeningSystems.Views;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.OpeningHUDs
{
    /// <summary>
    /// オープニング画面表示用クラス
    /// </summary>
    public class OpeningView : MonoBehaviour, IOpeningView
    {
        //Audio
        [Inject] private SEManager _seManager;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private RectTransform _titleLogo; //タイトルロゴ
        [SerializeField] private CanvasGroup _logoCanvasGroup; //タイトルロゴ
        [SerializeField] private CanvasGroup _startText; //スタートテキスト
        [SerializeField] private Transform _car; //車
        [SerializeField] private Transform _camera; //カメラ
        [SerializeField] private CanvasGroup _flash; //フラッシュ

        //キャンセル用
        private Sequence _playSequence; //play時のSequence
        private Sequence _finishSequence; //finish時のSequence


        public void PlayOpeningView()
        {
            //アニメーションが残っていたらキル
            _playSequence?.Kill(true);
            _finishSequence?.Kill(true);

            //初期設定
            _titleLogo.anchoredPosition = new Vector2(100f, 176f);

            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス：タイトルロゴ
            _playSequence = DOTween.Sequence()
                .Append(_titleLogo.DOAnchorPos(new Vector2(300f, 200f), 0.5f).SetEase(Ease.OutBack))
                .Join(_startText.DOFade(1f, 0.5f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _playSequence = null;
                });
        }


        public async UniTask FinishOpeningView()
        {
            //アニメーションが残っていたらキル
            _playSequence?.Kill(true);
            _finishSequence?.Kill(true);

            //演出シーケンス：タイトルロゴ、車、カメラ
            _finishSequence = DOTween.Sequence()
                //タイトルロゴを右上にフェードアウトしながら移動
                .Append(_titleLogo.DOAnchorPos(new Vector2(500f, 224f), 0.5f).SetEase(Ease.InBack))
                .Join(_logoCanvasGroup.DOFade(0f, 0.5f))
                //スタートテキストをフェードアウト
                .Join(_startText.DOFade(0f, 0.5f))
                //車を画面外へ移動
                .Append(_car.DOMoveX(13f, 0.5f).SetEase(Ease.OutSine))
                .JoinCallback(async () =>
                {
                    _seManager.PlaySE(SE.ShortKlaxon);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(0.2f));
                    _seManager.PlaySE(SE.ShortKlaxon);
                    _seManager.PlaySE(SE.Engine);
                })
                .Append(_car.DOMoveX(30f, 2f).SetEase(Ease.InCubic))
                .Join(_car.DOShakeRotation(1f, new Vector3(0f, 3f, 0f), 5, 0f, true))
                //カメラを上に向ける
                .Insert(1.4f, _camera.DORotate(new Vector3(-90f, 4.65f, 4.3f), 2f).SetEase(Ease.InOutCirc))
                //フラッシュ
                .Insert(2.5f, _flash.DOFade(1f, 1f).SetEase(Ease.OutSine))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _finishSequence = null;
                });
            await _finishSequence.AsyncWaitForCompletion();
        }
    }
}