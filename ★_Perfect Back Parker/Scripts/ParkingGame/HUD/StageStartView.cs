using Cysharp.Threading.Tasks;
using DG.Tweening;
using ParkingGame.Audio;
using ParkingGame.Data;
using ParkingGame.GameSystems;
using ParkingGame.GameSystems.View;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// ステージ開始時のViewを操作するクラス
    /// </summary>
    public class StageStartView : MonoBehaviour, IStageStartView
    {
        //Audio
        [Inject] private readonly SEManager _seManager;

        //View一覧
        [Header("View")]
        [SerializeField] private TMP_Text _textStageNum; //ステージ番号表示
        [SerializeField] private TMP_Text _textTimelimit; //制限時間表示

        //演出オブジェクト
        [Header("演出オブジェクト")]
        [Inject] private IPlayerSystem _player;
        [SerializeField] private Transform _gate; //ゲート
        [SerializeField] private Collider _gateColider; //ゲートのコライダー
        [SerializeField] private RectTransform _topLine; //上段UI
        [SerializeField] private RectTransform _middleLine; //中段UI
        [SerializeField] private RectTransform _bottomLine; //下段UI

        //キャンセル用
        private Sequence _modelSequence;
        private Sequence _UISequence;


        public async UniTask ShowStageInfomation(StageData stageData)
        {
            //Viewの初期設定
            _textStageNum.text = $"{stageData.StageName}";
            _textTimelimit.text = $"制限時間：{stageData.TimeLimit}秒";

            //アニメーションが残っていた場合キルする
            _modelSequence?.Kill(true);
            _UISequence?.Kill(true);

            //Viewを表示
            gameObject.SetActive(true);

            //演出シーケンス：プレイヤーとフィールド
            _modelSequence = DOTween.Sequence()
                .AppendCallback(() => _gateColider.isTrigger = true)
                .Append(_gate.DOLocalRotate(new Vector3(-90f, 0f, 0f), 1.5f))
                .Join(_player.Transform.DOLocalMoveZ(-11.44f, 2.8f).SetEase(Ease.InOutCubic))
                .Append(_gate.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f))
                .AppendCallback(() => _gateColider.isTrigger = false);

            //演出シーケンス：ステージ情報UI
            _UISequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _topLine.anchoredPosition = new Vector3(-1050f, 0f, 0f);
                    _middleLine.anchoredPosition = new Vector3(-1050f, 0f, 0f);
                    _bottomLine.anchoredPosition = new Vector3(-1050f, 0f, 0f);
                    _seManager.PlaySE(SE.Klaxon);
                })
                .Insert(0f, _topLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .Insert(0.2f, _middleLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .Insert(0.4f, _bottomLine.DOAnchorPosX(0f, 1f).SetEase(Ease.InOutElastic))
                .Insert(5f, _topLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                .Insert(5.2f, _middleLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                .Insert(5.4f, _bottomLine.DOAnchorPosX(-1050f, 1f).SetEase(Ease.InOutElastic))
                .AppendCallback(() => gameObject.SetActive(false));

            //3秒後には処理を返す
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
        }
    }
}