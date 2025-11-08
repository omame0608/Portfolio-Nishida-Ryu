using Alchemy.Inspector;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Users;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// クリアビュー
    /// </summary>
    public class ClearView : MonoBehaviour, IClearView
    {
        //システム
        [Inject] private readonly IPlayerCameraSystem _cameraSystem;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private RectTransform _clear; //Clearテキスト
        [SerializeField] private RectTransform _next; //Nextテキスト
        [SerializeField] private TMP_Text _nextText; //Nextテキスト
        [SerializeField] private RectTransform _baseScore; //基礎スコア
        [SerializeField] private TMP_Text _baseScoreText; //基礎スコアテキスト
        [SerializeField] private RectTransform _timeScore; //スピードクリア
        [SerializeField] private TMP_Text _timeScoreText; //スピードクリアテキスト
        [SerializeField] private RectTransform _noMissScore; //ノーミスクリア
        [SerializeField] private TMP_Text _noMissScoreText; //ノーミスクリアテキスト
        [SerializeField] private RectTransform _goldenScrewScore; //金のネジ取得
        [SerializeField] private TMP_Text _goldenScrewScoreText; //金のネジ取得テキスト
        [SerializeField] private RectTransform _waveAllScore; //ウェーブ合計スコア
        [SerializeField] private TMP_Text _waveAllScoreText; //ウェーブ合計スコアテキスト

        //Audio
        [Inject] private readonly SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _UISequence; //UIアニメーションシーケンス
        private Sequence _modelSequence; //モデルアニメーションシーケンス
        private Sequence _hideSequence; //UI非表示シーケンス

        //モデル操作後の片付け用
        private float _scale;
        private Vector3 _rot;


        public void ShowClearResult(WaveScores scores)
        {
            //アニメーションが残っていた場合キルする
            _UISequence?.Kill(true);
            _modelSequence?.Kill(true);
            _hideSequence?.Kill(true);

            //初期設定
            var scoresDeltaX = 800f;
            var headerDeltaX = 1050f;
            _clear.DOAnchorPosX(-headerDeltaX, 0f).SetRelative();
            _next.DOAnchorPosX(-200f, 0f).SetRelative();
            _nextText.alpha = 0f;
            _baseScore.DOAnchorPosX(-scoresDeltaX, 0f).SetRelative();
            _timeScore.DOAnchorPosX(-scoresDeltaX, 0f).SetRelative();
            _noMissScore.DOAnchorPosX(-scoresDeltaX, 0f).SetRelative();
            _goldenScrewScore.DOAnchorPosX(-scoresDeltaX, 0f).SetRelative();
            _waveAllScore.DOAnchorPosX(-headerDeltaX, 0f).SetRelative();

            //ウェーブのスコア一覧を表示
            _baseScoreText.text = $"基礎スコア　　：{scores.WaveBaseScore}";
            _timeScoreText.text = $"スピードクリア：{scores.TimeScore}";
            _noMissScoreText.text = $"ノーミスクリア：{scores.NoMissScore}";
            _goldenScrewScoreText.text = $"金のネジ取得　：{scores.GoldenScrewScore}";
            _waveAllScoreText.text = $"WAVEスコア：{scores.WaveTotalScore}";

            //Viewを表示
            gameObject.SetActive(true);

            //SE再生
            _seManager.PlaySE(SE.WaveClear);

            //演出シーケンス：UI
            var duration = 0.7f;
            _UISequence = DOTween.Sequence()
                //画面左側UIを順に表示
                .Append(_clear.DOAnchorPosX(headerDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                .Insert(0.05f, _baseScore.DOAnchorPosX(scoresDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                .Insert(0.10f, _timeScore.DOAnchorPosX(scoresDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                .Insert(0.15f, _noMissScore.DOAnchorPosX(scoresDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                .Insert(0.20f, _goldenScrewScore.DOAnchorPosX(scoresDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                .Insert(0.25f, _waveAllScore.DOAnchorPosX(headerDeltaX, duration).SetEase(Ease.InOutElastic).SetRelative())
                //ネクストテキストを表示
                .Append(DOTween.To(() => _nextText.alpha, a => _nextText.alpha = a, 1f, 1f))
                .Join(_next.DOAnchorPosX(200f, 1f).SetEase(Ease.OutQuart).SetRelative())
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _UISequence = null;
                });

            //演出シーケンス：モデル
            _modelSequence = DOTween.Sequence()
                //カメラの状態を記録
                .AppendCallback(() =>
                {
                    _scale = _cameraSystem.Transform.localScale.x;
                    _rot = _cameraSystem.Transform.localEulerAngles;
                })
                //モデルを操作して演出する
                .Append(_cameraSystem.Transform.DOScale(25f, 0.2f))
                .Join(_cameraSystem.Transform.DOLocalRotate(new Vector3(30f, 180f, 0f), 0.2f))
                .Append(_cameraSystem.Transform.DOLocalRotate(new Vector3(0f, 30f, 0f), 1f)
                            .SetEase(Ease.Linear)
                            .SetRelative()
                            .SetLoops(-1, LoopType.Incremental))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _modelSequence = null;
                });
        }


        public void HideClearResult(bool isFinalWave)
        {
            //アニメーションをキル
            _UISequence?.Kill(true);
            _modelSequence?.Kill(true);
            _hideSequence?.Kill(true);

            if (!isFinalWave)
            {
                //非表示シーケンス
                _hideSequence = DOTween.Sequence()
                    //動かしたモデルを元に戻す
                    .Append(_cameraSystem.Transform.DOScale(_scale, 0.2f))
                    .Join(_cameraSystem.Transform.DOLocalRotate(_rot, 0.2f))
                    //完了時にキャンセル用をクリア
                    .OnComplete(() =>
                    {
                        _hideSequence = null;
                    });
            }

            //Viewを非表示にする
            gameObject.SetActive(false);
        }


        [Title("単体テスト用")]
        [Button] private void TestShowClearResult()
        {
            ShowClearResult(new WaveScores(999, 999, 999, 999, 999));
        }
        [Button] private void TestHideClearResult() => HideClearResult(false);
    }
}