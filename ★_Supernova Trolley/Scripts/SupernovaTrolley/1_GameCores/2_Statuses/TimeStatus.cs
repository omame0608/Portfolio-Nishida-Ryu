using Cysharp.Threading.Tasks;
using System;
using UniRx;
using Utilities;

namespace SupernovaTrolley.GameCores.Statuses
{
    /// <summary>
    /// 時間を管理するステータスクラス
    /// </summary>
    public class TimeStatus : IDisposable
    {
        //ステータス
        private const int _MAX_TIME = 60; //最大時間
        public ReactiveProperty<int> RemainingTime = new(); //残り時間

        //タイマー0秒通知
        private Subject<Unit> _onTimerZero;
        public IObservable<Unit> OnTimerZero => _onTimerZero;

        //タイマー
        private CountdownTimer _countdownTimer;

        //購読解除用
        private CompositeDisposable _disposables;


        /// <summary>
        /// 時間ステータスの初期化
        /// </summary>
        public void InitStatus()
        {
            RemainingTime.Value = _MAX_TIME;
            _onTimerZero = new Subject<Unit>();
            _countdownTimer = new CountdownTimer();
            _disposables = new CompositeDisposable();
        }


        /// <summary>
        /// タイマーのカウントダウンを開始する
        /// </summary>
        public void StartTimer()
        {
            if (_countdownTimer.IsRunning) MyLogger.LogError($"タイマーがすでに動作しています");

            //タイマーにコールバックを登録
            _countdownTimer.OnTick
                .Subscribe(remainingTime =>
                {
                    RemainingTime.Value = remainingTime;
                    MyLogger.Log($"残り時間：{remainingTime}");
                })
                .AddTo(_disposables);
            _countdownTimer.OnComplete
                .Subscribe(_ =>
                {
                    _onTimerZero?.OnNext(Unit.Default);
                })
                .AddTo(_disposables);

            //タイマー開始
            _countdownTimer.StartTimer(_MAX_TIME).Forget();
        }


        /// <summary>
        /// タイマーを停止してリセットする
        /// </summary>
        public void ResetTimer()
        {
            //タイマー停止
            if (_countdownTimer.IsRunning)
            {
                _countdownTimer.CancelTimer();
            }

            //初期化
            _disposables.Clear();
            _countdownTimer.Dispose();
            _countdownTimer = new CountdownTimer();
        }


        void IDisposable.Dispose()
        {
            _onTimerZero?.Dispose();
            _countdownTimer?.Dispose();
            _disposables?.Dispose();
        }
    }
}