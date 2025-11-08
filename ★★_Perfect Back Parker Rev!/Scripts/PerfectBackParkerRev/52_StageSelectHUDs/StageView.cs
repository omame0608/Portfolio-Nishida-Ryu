using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.Directions.StageSelects;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PerfectBackParkerRev.StageSelectHUDs
{
    /// <summary>
    /// ステージ選択画面の個別UI
    /// (現在選択しているステージ個別のUI)
    /// </summary>
    public class StageView : MonoBehaviour, IStageView
    {
        

        [Header("操作対象のオブジェクト")]
        [SerializeField] private StageSelectPlayerMover _car; //車
        [SerializeField] private Transform _camera; //カメラ
        [SerializeField] private List<StageSelectMiniatureMover> _stageMiniatureList; //ミニチュアリスト
        [SerializeField] private RectTransform _stageName; //ステージ名
        [SerializeField] private TMP_Text _stageNameText; //ステージ名テキスト
        [SerializeField] private RectTransform _bestScore; //ベストスコア
        [SerializeField] private TMP_Text _bestScoreText; //ベストスコアテキスト
        [SerializeField] private RectTransform _startButton; //スタートボタン
        [SerializeField] private RectTransform _overView; //ステージ説明
        [SerializeField] private TMP_Text _overViewText; //ステージ説明テキスト
        [SerializeField] private CanvasGroup _overViewCanvas; //ステージ説明

        //キャンセル用
        private Sequence _inSceneSequence; //シーン遷移時用のSequence
        private Sequence _nextMoveSequence; //次のステージへ移動するsequence
        private Sequence _previousMoveSequence; //前のステージへ移動するsequence
        private Sequence _stageInformationSequence; //ステージ情報を表示するsequence


        public async UniTask ShowInSceneView(int stageNumber, StageData stageData, int highScore)
        {
            //アニメーションが残っていたらキル
            _inSceneSequence?.Kill(true);
            _nextMoveSequence?.Kill(true);
            _previousMoveSequence?.Kill(true);

            //初期設定
            int stageListIndex = stageNumber - 1; //初期選択のステージミニチュアのインデックス
            var miniature = _stageMiniatureList[stageListIndex]; //初期選択ステージのミニチュア

            //演出シーケンス
            _inSceneSequence = DOTween.Sequence()
                //ミニチュアを変形する
                .Append(miniature.transform.DOScale(1f, 1f).SetEase(Ease.OutQuart))
                .Join(miniature.transform.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutQuart))
                .JoinCallback(() => miniature.PlayAnimation())
                //ステージ情報を表示する
                .JoinCallback(() => ShowStageInformation(stageData, highScore))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _inSceneSequence = null;
                });
            await _inSceneSequence.AsyncWaitForCompletion();
        }


        public async UniTask ShowNextStageView(int stageNumber, StageData stageData, int highScore)
        {
            //アニメーションが残っていたらキル
            _inSceneSequence?.Kill(true);
            _nextMoveSequence?.Kill(true);
            _previousMoveSequence?.Kill(true);

            //初期設定
            int stageListIndex = stageNumber - 1; //移動後のステージミニチュアのインデックス
            var previousMiniature = _stageMiniatureList[stageListIndex - 1]; //移動前のミニチュア
            var currentMiniature = _stageMiniatureList[stageListIndex]; //移動後のミニチュア

            //演出シーケンス
            _nextMoveSequence = DOTween.Sequence()
                //車とカメラを移動させる
                .Append(_car.transform.DOMoveX(-5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                .JoinCallback(() => _car.PlayMoveDirection(true, 1f))
                .Join(_camera.DOMoveX(-5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                //移動前のミニチュアを変形する
                .Join(previousMiniature.transform.DOScale(0.5f, 1f).SetEase(Ease.OutQuart))
                .Join(previousMiniature.transform.DORotate(new Vector3(0f, -180f, 0f), 1f).SetEase(Ease.OutQuart))
                .JoinCallback(() => previousMiniature.FinishAnimation())
                //移動後のミニチュアを変形する
                .Join(currentMiniature.transform.DOScale(1f, 1f).SetEase(Ease.OutQuart))
                .Join(currentMiniature.transform.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutQuart))
                .JoinCallback(() => currentMiniature.PlayAnimation())
                //ステージ情報を表示する
                .JoinCallback(() => ShowStageInformation(stageData, highScore))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _nextMoveSequence = null;
                });
            await _nextMoveSequence.AsyncWaitForCompletion();
        }


        public async UniTask ShowPreviousStageView(int stageNumber, StageData stageData, int highScore)
        {
            //アニメーションが残っていたらキル
            _inSceneSequence?.Kill(true);
            _nextMoveSequence?.Kill(true);
            _previousMoveSequence?.Kill(true);

            //初期設定
            int stageListIndex = stageNumber - 1; //移動後のステージミニチュアのインデックス
            var previousMiniature = _stageMiniatureList[stageListIndex + 1]; //移動前のミニチュア
            var currentMiniature = _stageMiniatureList[stageListIndex]; //移動後のミニチュア

            //演出シーケンス
            _previousMoveSequence = DOTween.Sequence()
                //車とカメラを移動させる
                .Append(_car.transform.DOMoveX(5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                .JoinCallback(() => _car.PlayMoveDirection(false, 1f))
                .Join(_camera.DOMoveX(5f, 1f).SetEase(Ease.OutQuart).SetRelative())
                //移動前のミニチュアを変形する
                .Join(previousMiniature.transform.DOScale(0.5f, 1f).SetEase(Ease.OutQuart))
                .Join(previousMiniature.transform.DORotate(new Vector3(0f, -180f, 0f), 1f).SetEase(Ease.OutQuart))
                .JoinCallback(() => previousMiniature.FinishAnimation())
                //移動後のミニチュアを変形する
                .Join(currentMiniature.transform.DOScale(1f, 1f).SetEase(Ease.OutQuart))
                .Join(currentMiniature.transform.DORotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutQuart))
                .JoinCallback(() => currentMiniature.PlayAnimation())
                //ステージ情報を表示する
                .JoinCallback(() => ShowStageInformation(stageData, highScore))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _previousMoveSequence = null;
                });
            await _previousMoveSequence.AsyncWaitForCompletion();
        }


        /// <summary>
        /// ステージ情報を表示する
        /// </summary>
        /// <param name="stageData">ステージ番号</param>
        /// <param name="highScore">ハイスコア</param>
        private void ShowStageInformation(StageData stageData, int highScore)
        {
            //アニメーションが残っていたらキル
            _stageInformationSequence?.Kill(true);

            //ステージ情報を取得
            var stageName = stageData.StageName;
            var overView = stageData.OverView;

            //初期設定
            _stageName.anchoredPosition = new Vector2(1400f, 40f);
            _bestScore.anchoredPosition = new Vector2(1400f, -80f);
            _bestScoreText.text = $"{highScore}";
            _startButton.anchoredPosition = new Vector2(1200f, -230f);
            _overView.anchoredPosition = new Vector2(100f, -430f);
            _overViewCanvas.alpha = 0f;
            _stageNameText.text = stageName;
            _overViewText.text = overView;

            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス
            _stageInformationSequence = DOTween.Sequence()
                //UIを画面内に移動
                .Insert(0f, _stageName.DOAnchorPosX(780f, 0.5f).SetEase(Ease.OutQuint))
                .Insert(0.1f, _bestScore.DOAnchorPosX(780f, 0.5f).SetEase(Ease.OutQuint))
                .Insert(0.2f, _startButton.DOAnchorPosX(680f, 0.5f).SetEase(Ease.OutQuint))
                .Insert(0.3f, _overView.DOAnchorPosX(0f, 0.5f).SetEase(Ease.OutQuint))
                .Join(_overViewCanvas.DOFade(1f, 0.5f))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _stageInformationSequence = null;
                });
        }
    }
}