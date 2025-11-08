using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.Utilities;
using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev.Notifiers
{
    /// <summary>
    /// ゴールの判定
    /// </summary>
    public class GoalDetector : MonoBehaviour, IWaveFinishNotifier
    {
        //システム
        [Inject] private readonly IClearCountView _clearCountView;

        //一定時間ゴール枠に留まると発火
        private readonly Subject<Unit> _onWaveFinishDetected = new();
        public IObservable<Unit> OnWaveFinishDetected => _onWaveFinishDetected;

        //判定用
        private Bounds _goalBounds; //ゴール領域
        private Bounds _carBounds; //プレイヤー領域
        private const int _ANGLE_THRESHOLD = 15; //ゴール枠に対する車の向きの閾値(度)

        //カウント用
        private readonly CountdownTimer _countdownTimer = new();
        private const int _TIME = 3; //ゴール成功までの秒数
        private bool _isClear = false; //ウェーブクリア済みフラグ


        private void Awake()
        {
            //自身のバウンズを取得
            _goalBounds = GetComponent<Collider>().bounds;

            //タイマーにコールバックを登録
            //ゴール枠に留まっている間毎秒発火
            _countdownTimer.OnTick
                .Where(remainingTime => remainingTime > 0)
                .Subscribe(remainingTime =>
                {
                    //カウントビューを表示
                    _clearCountView.ShowCountOnce(remainingTime);
                }).AddTo(this);
            //カウントダウン完了時に発火
            _countdownTimer.OnComplete.Subscribe(_ =>
            {
                //片づけてクリアを通知
                _clearCountView.Cancel();
                _isClear = true;
                _onWaveFinishDetected.OnNext(Unit.Default);
            }).AddTo(this);
        }


        private void OnTriggerStay(Collider other)
        {
            if (_isClear) return;
            if (!other.CompareTag("GoalCollider")) return;
            if (!CheckCarDirection(other.transform, transform)) return;

            //プレイヤーの車のバウンズを取得
            _carBounds = other.bounds;

            //プレイヤーの車がゴール領域内に完全に収まっているか判定
            if (_goalBounds.Contains(_carBounds.min) && _goalBounds.Contains(_carBounds.max))
            {
                //タイマーが動作していなければゴールカウント開始
                if (!_countdownTimer.IsRunning)
                {
                    _countdownTimer.StartTimer(_TIME).Forget();
                }
            }
            //はみ出していたらタイマーリセット
            else
            {
                //タイマーが動作していればキャンセル
                if (_countdownTimer.IsRunning)
                {
                    _countdownTimer.CancelTimer();
                    _clearCountView.Cancel();
                }
            }
        }


        /// <summary>
        /// 車がゴール枠で指定された方向を向いているか判定
        /// </summary>
        /// <param name="car">プレイヤー</param>
        /// <param name="flame">ゴール枠</param>
        /// <returns>バック駐車かどうか</returns>
        private bool CheckCarDirection(Transform car, Transform flame)
        {
            //車とゴール枠がなす角が一定未満ならバック駐車と判定
            var angle = Vector3.Angle(car.forward, flame.right);
            return  angle < _ANGLE_THRESHOLD;
        }


        private void OnDestroy()
        {
            _onWaveFinishDetected.Dispose();
        }
    }
}