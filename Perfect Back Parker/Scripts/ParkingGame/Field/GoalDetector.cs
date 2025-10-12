using Cysharp.Threading.Tasks;
using ParkingGame.GameSystems;
using ParkingGame.GameSystems.View;
using ParkingGame.HUD;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace ParkingGame.Field
{
    /// <summary>
    /// ステージクリアを検知するクラス
    /// </summary>
    public class GoalDetector : MonoBehaviour
    {
        //参照
        [Inject] private readonly IClearCountView _clearCountView;

        //ゴール判定時のコールバック関数
        private Subject<Unit> _onGoal = new Subject<Unit>();
        public IObservable<Unit> OnGoal => _onGoal;

        //判定用のバウンズ
        private Bounds _goalBounds; //ゴール側領域
        private Bounds _carBounds; //プレイヤー側領域

        //処理用
        private const int _TIME = 3; //ステージクリアに必要な滞在時間
        private CountdownTimer _timer = new CountdownTimer();
        private IDisposable _disposableClearConutTick;
        private IDisposable _disposableClearConutComplete;
        private bool _isClear; //ステージをクリアしたかどうか


        void Start()
        {
            //初期化
            _goalBounds = GetComponent<Collider>().bounds;
            _isClear = false;

            //クリア判定後の処理
            _timer.OnComplete.Subscribe(_ =>
            {
                _isClear = true;
                _onGoal.OnNext(Unit.Default);
            }).AddTo(this);
        }


        private void OnTriggerStay(Collider other)
        {
            if (!other.transform.CompareTag("GoalCollider")) return;
            if (_isClear) return;
            if (!CheckCarDirection(other.transform, transform)) return;

            //車のバウンズを取得
            _carBounds = other.bounds;

            //プレイヤーがゴール内に完全に収まっている場合
            if (_goalBounds.Contains(_carBounds.min) && _goalBounds.Contains(_carBounds.max))
            {
                //タイマーが動いていなければカウント開始
                if (!_timer.IsRunning)
                {
                    _disposableClearConutTick = _timer.OnTick
                        .Where(count => count > 0)
                        .Subscribe(count => _clearCountView.ShowCountOnce(count))
                        .AddTo(this);
                    _disposableClearConutComplete = _timer.OnComplete
                        .Subscribe(count => _clearCountView.Cancel())
                        .AddTo(this);
                    _timer.StartTimer(_TIME).Forget();
                }
            }
            else
            {
                //タイマーが動作中なら停止する
                if (_timer.IsRunning)
                {
                    _disposableClearConutTick.Dispose();
                    _disposableClearConutComplete.Dispose();
                    _clearCountView.Cancel();
                    _timer.CancelTimer();
                }
            }
        }


        /// <summary>
        /// 車の方向を判定
        /// </summary>
        /// <param name="car">プレイヤー</param>
        /// <param name="flame">ゴールフレーム</param>
        /// <returns>バック駐車かどうか</returns>
        private bool CheckCarDirection(Transform car, Transform flame)
        {
            float d = Vector3.Angle(car.forward, flame.right);
            return d < 45f;
        }


        void OnDestroy()
        {
            _onGoal?.Dispose();
            _timer?.Dispose();
        }
    }
}