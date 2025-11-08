using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PerfectBackParkerRev.Directions.Games
{
    /// <summary>
    /// フィールド内を動く車
    /// </summary>
    public class MovingCar : MonoBehaviour
    {
        [Title("車の制御パラメータ")]
        [SerializeField, Header("移動距離")] private float _distance = 4.5f;
        [SerializeField, Header("移動にかかる時間")] private float _duration = 3f;
        [SerializeField, Header("移動先での停止時間")] private float _stopTimeOnMoved = 0.5f;
        [SerializeField, Header("移動後の停止時間")] private float _stopTimeOnFinished = 2f;
        [SerializeField, Header("動き始めまでのオフセット")] private float _startDelay = 1f;
        [SerializeField, Header("移動の補間(前半)")] private Ease _beforeEasing = Ease.InOutSine;
        [SerializeField, Header("移動の補間(後半)")] private Ease _afterEasing = Ease.InOutSine;

        [Title("制御用")]
        [SerializeField] private List<GameObject> wheelList = new();


        private async void Start()
        {
            //オブジェクト破棄でキャンセルされるトークン
            var ct = this.GetCancellationTokenOnDestroy();

            try
            {
                //動き始めまでの遅延
                await UniTask.Delay(TimeSpan.FromSeconds(_startDelay), cancellationToken: ct);

                //正面方向に向かう大きさ_distanceのベクトルを作成
                Vector3 forward = transform.forward * _distance;

                //動作シーケンス
                _ = DOTween.Sequence()
                    //前進
                    .Append(transform.DOLocalMove(forward, _duration).SetRelative().SetEase(_beforeEasing))
                    .JoinCallback(() =>
                    {
                        foreach (var wheel in wheelList)
                        {
                            wheel.transform.DOLocalRotate(new Vector3(180f * _distance, 0f, 0f), _duration)
                                .SetRelative()
                                .SetEase(_beforeEasing)
                                .SetLink(gameObject);
                        }
                    })
                    //移動先で一定時間停止
                    .AppendInterval(_stopTimeOnMoved)
                    //後退
                    .Append(transform.DOLocalMove(-forward, _duration).SetRelative().SetEase(_afterEasing))
                    .JoinCallback(() =>
                    {
                        foreach (var wheel in wheelList)
                        {
                            wheel.transform.DOLocalRotate(new Vector3(-180f * _distance, 0f, 0f), _duration)
                                .SetRelative()
                                .SetEase(_beforeEasing)
                                .SetLink(gameObject);
                        }
                    })
                    //初期位置で一定時間停止
                    .AppendInterval(_stopTimeOnFinished)
                    .SetLoops(-1)
                    .SetLink(gameObject);
            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}