using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace PerfectBackParkerRev.Directions.Games
{
    /// <summary>
    /// 動く電車
    /// </summary>
    public class MovingTrain : MonoBehaviour
    {
        [Title("車の制御パラメータ")]
        [SerializeField, Header("移動にかかる時間")] private float _duration = 15f;
        [SerializeField, Header("移動後の停止時間")] private float _stopTimeOnFinished = 5f;
        [SerializeField, Header("動き始めまでのオフセット")] private float _startDelay = 5f;


        private async void Start()
        {
            //オーディオソースを取得
            var audioSource = GetComponent<AudioSource>();

            //オブジェクト破棄でキャンセルされるトークン
            var ct = this.GetCancellationTokenOnDestroy();

            try
            {
                //動き始めまでの遅延
                await UniTask.Delay(TimeSpan.FromSeconds(_startDelay), cancellationToken:ct);

                //動作シーケンス
                _ = DOTween.Sequence()
                    //前進
                    .Append(transform.DOLocalMoveX(-500f, _duration).SetRelative().SetEase(Ease.Linear))
                    .InsertCallback(_duration * 0.3f, () =>
                    {
                        //汽笛SE
                        audioSource.Play();
                    })
                    //移動先で一定時間停止
                    .AppendInterval(_stopTimeOnFinished)
                    //スタート地点に戻る
                    .AppendCallback(() =>
                    {
                        transform.localPosition = new Vector3(-250f, transform.localPosition.y, transform.localPosition.z);
                    })
                    .SetLoops(-1)
                    .SetLink(gameObject);
            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}