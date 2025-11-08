using Cysharp.Threading.Tasks;
using DG.Tweening;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Directions.StageSelects;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.StageSelectHUDs
{
    /// <summary>
    /// ステージ選択画面の共通UI
    /// (現在選択しているステージに依らないUI)
    /// </summary>
    public class StageSelectView : MonoBehaviour, IStageSelectView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        [Header("操作対象のオブジェクト")]
        [SerializeField] private StageSelectPlayerMover _car; //車
        [SerializeField] private Transform _camera; //カメラ
        [SerializeField] private RectTransform _leftArrow; //左矢印ボタン
        [SerializeField] private RectTransform _rightArrow; //右矢印ボタン
        [SerializeField] private RectTransform _allScore; //総スコアUI
        [SerializeField] private TMP_Text _allScoreText; //総スコアテキスト
        [SerializeField] private List<GameObject> _stopPollsList; //停止ポールリスト

        [Header("見た目調整用")]
        private float _carDeltaPositionX = 6f; //車のx座標の初期位置
        private float _uiStartPositionY = 600f;
        private float _uiEndPositionY = 380f;

        //キャンセル用
        private Sequence _inSequence; //車を動かすSequence


        public async UniTask ShowStageSelectView(int currentStageNumber, int totalGameScore, int currentLatestStageNumber)
        {
            //アニメーションが残っていたらキル
            _inSequence?.Kill(true);

            //初期設定
            _ = _car.transform.DOMoveX(-5 * (currentStageNumber - 1) + _carDeltaPositionX, 0f).SetRelative();
            _ = _camera.DOMoveX(-5 * (currentStageNumber - 1), 0f).SetRelative();
            _leftArrow.anchoredPosition = new Vector2(_leftArrow.anchoredPosition.x, -_uiStartPositionY);
            _rightArrow.anchoredPosition = new Vector2(_rightArrow.anchoredPosition.x, -_uiStartPositionY);
            _allScore.anchoredPosition = new Vector2(_allScore.anchoredPosition.x, _uiStartPositionY);
            _allScoreText.text = $"総スコア:{totalGameScore}";
            for (int i = 0; i < currentLatestStageNumber-1; i++)
            {
                _stopPollsList[i].SetActive(false);
            }

            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス
            _inSequence = DOTween.Sequence()
                //車を画面内に移動
                .Append(_car.transform.DOMoveX(-_carDeltaPositionX, 1.5f).SetEase(Ease.InOutBack).SetRelative())
                .JoinCallback(async () => 
                {
                    _car.PlayInSceneDirection(1.5f);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                    _seManager.PlaySE(SE.Brake);
                })
                //UIを画面内に移動
                .Append(_leftArrow.DOAnchorPosY(-_uiEndPositionY, 0.5f).SetEase(Ease.OutQuart))
                .Join(_rightArrow.DOAnchorPosY(-_uiEndPositionY, 0.5f).SetEase(Ease.OutQuart))
                .Join(_allScore.DOAnchorPosY(_uiEndPositionY, 0.5f).SetEase(Ease.OutQuart))
                //完了時にキャンセル用をクリア
                .OnComplete(() =>
                {
                    _inSequence = null;
                });
            await _inSequence.AsyncWaitForCompletion();
        }
    }
}