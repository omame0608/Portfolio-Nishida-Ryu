using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PerfectBackParkerRev.GameHUDs
{
    /// <summary>
    /// ステージクリア画面のView
    /// </summary>
    public class StageFinishView : MonoBehaviour, IStageFinishView
    {
        [Header("操作対象のオブジェクト")]
        [SerializeField] private GameObject[] _setNonActives; //非アクティブにするオブジェクト群
        [SerializeField] private GameObject _results; //結果UI
        [SerializeField] private TMP_Text _stageClearText; //ステージクリアテキスト
        [SerializeField] private TMP_Text[] _waveScoreTexts; //ウェーブごとのスコア表示テキスト群
        [SerializeField] private TMP_Text _stageScoreText; //ステージスコア表示テキスト
        [SerializeField] private Image[] _goldenScrewIcons; //金のネジアイコン群
        [SerializeField] private GameObject _directionCamera; //演出用カメラ
        [SerializeField] private RectTransform[] _waveScores; //ウェーブスコア表示群
        [SerializeField] private CanvasGroup[] _waveScoreCanvases; //ウェーブスコア表示群のキャンバスグループ群
        [SerializeField] private RectTransform _stageScore; //ステージスコア表示
        [SerializeField] private CanvasGroup _stageScoreCanvas; //ステージスコア
        [SerializeField] private RectTransform _endText; //ステージ終了テキスト
        [SerializeField] private CanvasGroup _endTextCanvas; //ステージ終了テキストキャンバスグループ

        //Audio
        [Inject] private SEManager _seManager; //SEマネージャー

        //キャンセル用
        private Sequence _sequence;


        public async UniTask ShowStageResult(int stageNumber, List<WaveScores> waveScores,
                                             int stageScore, bool?[] goldenScrews)
        {
            //アニメーションが残っていた場合キルする
            _sequence?.Kill(true);

            //初期設定
            foreach (var score in _waveScores)
            {
                score.localPosition -= new Vector3(150f, 150f * (float)Math.Tan(7f), 0f);
            }
            foreach (var canvas in _waveScoreCanvases)
            {
                canvas.alpha = 0f;
            }
            _stageScore.localScale = new Vector3(2f, 2f, 2f);
            _stageScoreCanvas.alpha = 0f;
            _endText.anchoredPosition -= new Vector2(100f, 0f);
            _endTextCanvas.alpha = 0f;

            //表示するUIを設定
            _stageClearText.text = $"ステージ{stageNumber} クリア!!";
            for (int i = 0; i < _waveScoreTexts.Length; i++)
            {
                if (i >= waveScores.Count) continue; //テスト用
                //各ウェーブのスコアを設定
                _waveScoreTexts[i].text = $"{waveScores[i].WaveTotalScore}";
            }
            _stageScoreText.text = $"{stageScore}";
            for (int i = 0; i < _goldenScrewIcons.Length; i++)
            {
                if (i >= goldenScrews.Length) continue; //テスト用
                //金のネジ取得状況に応じてアイコンの色を変更
                if (!(goldenScrews[i] ?? false))
                {
                    _goldenScrewIcons[i].color = Color.black;
                }
            }

            //Viewを表示、一部Viewを非表示
            gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _directionCamera.SetActive(true);
            _directionCamera.GetComponentInChildren<Camera>().depth = 10;
            _results.SetActive(true);
            foreach (var obj in _setNonActives)
            {
                obj.SetActive(false);
            }

            //SE再生
            _seManager.PlaySE(SE.StageClear);

            //演出シーケンス
            _sequence = DOTween.Sequence()
                //一定時間待機
                .AppendInterval(1f)
                //ウェーブスコアを順番に表示
                .AppendCallback(async () =>
                {
                    foreach (var score in _waveScores)
                    {
                        //SE再生
                        _seManager.PlaySE(SE.WaveScoreResult);

                        _ = score.DOAnchorPos(new Vector2(150f, 150f*(float)Math.Tan(7f)), 0.3f).SetEase(Ease.OutExpo).SetRelative();
                        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                    }
                })
                .JoinCallback(async () =>
                {
                    foreach (var canvas in _waveScoreCanvases)
                    {
                        _=canvas.DOFade(1f, 0.3f);
                        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                    }
                })
                .AppendInterval(1.5f)
                //ステージスコアを表示
                .Append(_stageScore.DOScale(1.15f, 0.7f).SetEase(Ease.InExpo))
                .Join(_stageScoreCanvas.DOFade(1f, 0.2f))
                .Append(_stageScore.DOShakeAnchorPos(0.5f, 50f))
                .JoinCallback(() =>
                {
                    //SE再生
                    _seManager.PlaySE(SE.StageScoreResult);
                })
                //エンドテキストを表示
                .AppendInterval(0.5f)
                .Append(_endText.DOAnchorPosX(100f, 0.5f).SetEase(Ease.OutQuart).SetRelative())
                .Join(_endTextCanvas.DOFade(1f, 0.5f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _sequence = null;
                })
                .SetLink(gameObject);
            await _sequence.AsyncWaitForCompletion().AsUniTask();
        }


        [Title("単体テスト用")]
        [Button] private void TestShowStageResult() => _=ShowStageResult(9,
                                                                       new List<WaveScores>()
                                                                       {
                                                                           new WaveScores(100, 100, 100, 100, 7777),
                                                                           new WaveScores(100, 100, 100, 100, 7777),
                                                                           new WaveScores(100, 100, 100, 100, 7777)
                                                                       },
                                                                       77777,
                                                                       new bool?[] { true, false, false });
    }
}