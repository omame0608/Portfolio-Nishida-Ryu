using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Directions.StageSelects;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using PerfectBackParkerRev.GameHUDs;
using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.StageSelectHUDs
{
    /// <summary>
    /// エンディングビュー
    /// </summary>
    public class EndingView : MonoBehaviour, IEndingView
    {
        //システム
        [Inject] private ClearCountView _clearCountView;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private RectTransform _stageSelectView;
        [SerializeField] private RectTransform _stageView;
        [SerializeField] private StageSelectPlayerMover _car; //車
        [SerializeField] private Transform _camera; //カメラ
        [SerializeField] private GameObject _results;
        [SerializeField] private GameObject _front;
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private TMP_Text[] _stageHighScoreTexts;
        [SerializeField] private CanvasGroup[] _stageHighScoreCanvases;
        [SerializeField] private RectTransform[] _stageHighScores;
        [SerializeField] private CanvasGroup _totalScoreCanvas;
        [SerializeField] private RectTransform _totalScore;
        [SerializeField] private CanvasGroup _gameBackCanvas;
        [SerializeField] private RectTransform _gameBackText;

        //Audio
        [Inject] private readonly BGMManager _bgmManager;
        [Inject] private readonly SEManager _seManager;

        //キャンセル用
        private Sequence _inSequence;
        private Sequence _outSequence;


        public async UniTask ShowEndingView(int[] stageScores, int totalHighScore)
        {
            //アニメーションが残っていたらキル
            _inSequence?.Kill(true);
            _outSequence?.Kill(true);

            //初期設定
            _totalScoreText.text = $"{totalHighScore}";
            for (int i = 0; i < _stageHighScoreTexts.Length; i++)
            {
                _stageHighScoreTexts[i].text = $"{stageScores[i]}";
            }
            foreach (var score in _stageHighScores)
            {
                score.localPosition -= new Vector3(150f, 150f * (float)Math.Tan(7f), 0f);
            }
            foreach (var canvas in _stageHighScoreCanvases)
            {
                canvas.alpha = 0f;
            }
            _totalScore.localScale = new Vector3(2f, 2f, 2f);
            _totalScoreCanvas.alpha = 0f;
            _gameBackText.anchoredPosition -= new Vector2(100f, 0f);
            _gameBackCanvas.alpha = 0f;

            //Viewを表示
            gameObject.SetActive(true);

            //BGMを停止
            _bgmManager.StopBGM(true);

            //演出シーケンス
            _inSequence = DOTween.Sequence()
                //一定時間待機
                .AppendInterval(0.5f)
                //通常時Viewを画面外へ移動する
                .Append(_stageSelectView.DOScale(3.5f, 0.5f).SetEase(Ease.InExpo))
                .Join(_stageView.DOScale(3.5f, 0.5f).SetEase(Ease.InExpo))
                //プレイヤーとカメラをゴール枠へ移動させる
                .Append(_car.transform.DOMoveX(-5f, 3f).SetEase(Ease.OutQuart).SetRelative())
                .JoinCallback(() =>
                {
                    _car.PlayMoveDirection(isMoveNext: true, duration: 3.0f);
                })
                .Join(_camera.DOMoveX(-5f, 3f).SetEase(Ease.OutQuart).SetRelative())
                //クリアカウントを表示
                .AppendCallback(() => _clearCountView.CanDisplayCircle = true)
                .AppendCallback(() => _clearCountView.ShowCountOnce(3))
                .AppendInterval(1f)
                .AppendCallback(() => _clearCountView.ShowCountOnce(2))
                .AppendInterval(1f)
                .AppendCallback(() => _clearCountView.ShowCountOnce(1))
                .AppendInterval(1f)
                .AppendCallback(() => _clearCountView.CanDisplayCircle = false)
                //エンディング画面を表示
                .AppendCallback(() =>
                {
                    _front.SetActive(true);
                })
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    _results.SetActive(true);
                    _seManager.PlaySE(SE.GameClear);
                })
                //各ステージスコアを順番に表示
                .AppendInterval(0.5f)
                .AppendCallback(async () =>
                {
                    foreach (var score in _stageHighScores)
                    {
                        //SE再生
                        _seManager.PlaySE(SE.WaveScoreResult);
                        _ = score.DOAnchorPos(new Vector2(150f, 150f * (float)Math.Tan(7f)), 0.5f).SetEase(Ease.OutExpo).SetRelative();   
                        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
                    }
                })
                .AppendCallback(async () =>
                {
                    foreach (var canvas in _stageHighScoreCanvases)
                    {
                        _ = canvas.DOFade(1f, 0.5f);
                        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
                    }
                })
                .AppendInterval(1.5f)
                //ステージスコアを表示
                .Append(_totalScore.DOScale(1.15f, 0.7f).SetEase(Ease.InExpo))
                .Join(_totalScoreCanvas.DOFade(1f, 0.2f))
                .Append(_totalScore.DOShakeAnchorPos(0.5f, 50f))
                .JoinCallback(() =>
                {
                    //SE再生
                    _seManager.PlaySE(SE.StageScoreResult);
                })
                //ゲームに戻るテキストを表示
                .AppendInterval(0.5f)
                .Append(_gameBackText.DOAnchorPosX(100f, 0.5f).SetEase(Ease.OutQuart).SetRelative())
                .Join(_gameBackCanvas.DOFade(1f, 0.5f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _inSequence = null;
                });
            await _inSequence.AsyncWaitForCompletion();
        }

        public async UniTask HideEndingView()
        {
            //アニメーションが残っていたらキル
            _inSequence?.Kill(true);
            _outSequence?.Kill(true);

            //演出シーケンス
            _outSequence = DOTween.Sequence()
                //frontビューをサイド表示させる
                .AppendCallback(() =>
                {
                    _front.SetActive(false);
                    _front.SetActive(true);
                })
                .AppendInterval(0.5f)
                //ビューを非表示にしてBGMを再生する
                .AppendCallback(() =>
                {
                    _results.SetActive(false);
                    _bgmManager.PlayBGM(BGM.StageSelect);
                })
                //プレイヤーとカメラをもとの場所へ移動させる
                .Append(_car.transform.DOMoveX(5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                .JoinCallback(() =>
                {
                    _car.PlayMoveDirection(isMoveNext: false, duration: 1.0f);
                })
                .Join(_camera.DOMoveX(5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                //通常時Viewを画面中央へ戻す
                .Append(_stageSelectView.DOScale(1f, 0.5f).SetEase(Ease.OutExpo))
                .Join(_stageView.DOScale(1f, 0.5f).SetEase(Ease.OutExpo))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _inSequence = null;
                });
            await _outSequence.AsyncWaitForCompletion();

            //Viewを非表示
            _front.SetActive(false);
            _results.SetActive(false);
            gameObject.SetActive(false);

            
        }
    }
}