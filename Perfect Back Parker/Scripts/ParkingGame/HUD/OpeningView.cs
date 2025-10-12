using Cysharp.Threading.Tasks;
using DG.Tweening;
using ParkingGame.GameSystems;
using ParkingGame.GameSystems.View;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ParkingGame.HUD
{
    /// <summary>
    /// オープニング画面のView
    /// </summary>
    public class OpeningView : MonoBehaviour, IOpeningView
    {
        //参照
        [Inject] private ICameraSystem _camera;

        //View一覧
        [Header("View")]
        [SerializeField] private RectTransform _titleLogo; //ロゴ
        [SerializeField] private RectTransform _start; //スタートテキスト

        //キャンセル用
        private Tweener _cameraTweener;
        private Sequence _uiSequence;


        public void PlayOpeningView()
        {
            //Viewを表示
            gameObject.SetActive(true);

            //カメラを回転
            _camera.Transform.rotation = Quaternion.Euler(new Vector3(-70f, 111f, 0f));
            _cameraTweener = _camera.Transform.DORotate(new Vector3(0f, 15f, 0f), 1f).SetEase(Ease.Linear)
                            .SetRelative().SetLoops(-1, LoopType.Incremental);

            //演出シーケンス：UI
            _uiSequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _titleLogo.anchoredPosition = new Vector3(-1502f, -396f, 0f);
                    _start.anchoredPosition = new Vector3(-1109f, -604f, 0f);
                })
                .Append(_titleLogo.DOAnchorPos(new Vector3(500f, -150f, 0f), 1f).SetEase(Ease.InOutBack))
                .Insert(0.2f, _start.DOAnchorPos(new Vector3(550f, -400f, 0f), 1f).SetEase(Ease.InOutBack));
        }

        public async UniTask FinishOpeningView()
        {
            //アニメーションをキル
            _cameraTweener.Kill(true);
            _uiSequence.Kill(true);

            //カメラの向きを初期化
            _ = _camera.Transform.DORotate(new Vector3(-45f, 45f, 0f), 1f);
            _ = _camera.Transform.DOScale(new Vector3(25f, 25f, 25f), 1f);

            //演出シーケンス：UI
            _ = DOTween.Sequence()
                .Append(_titleLogo.DOAnchorPos(new Vector3(1525f, -24f, 0f), 1f).SetEase(Ease.InOutBack))
                .Insert(0.2f, _start.DOAnchorPos(new Vector3(1371f, -300f, 0f), 1f).SetEase(Ease.InOutBack));

            //1.5秒後には処理を返す
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            gameObject.SetActive(false);
        }
    }
}