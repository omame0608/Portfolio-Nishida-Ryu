using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.Utilities;
using System;
using UniRx;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems
{
    /// <summary>
    /// ウェーブの時間制限管理専用のタイマー
    /// </summary>
    public class WaveTimer : IWaveFinishNotifier, IDisposable
    {
        //システム
        [Inject] private readonly IWaveTimerView _waveTimerView;

        //タイムオーバーで発火
        private readonly Subject<Unit> _onWaveFinishDetected = new();
        public IObservable<Unit> OnWaveFinishDetected => _onWaveFinishDetected;

        //タイマー処理用
        private CountdownTimer _countdownTimer = new();
        private bool _isTimerRunning => _countdownTimer.IsRunning;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();

        //残り時間を保持
        private int _remainingTime;
        public int RemainingTime => _remainingTime;


        /// <summary>
        /// ウェーブの制限時間タイマーを開始する
        /// </summary>
        /// <param name="timelimit">制限時間(秒)</param>
        public void StartWaveTimer(int timelimit)
        {
            if (_isTimerRunning) throw new InvalidOperationException("既に動作中のWaveTimerです");

            //タイマーにコールバックを登録
            //毎秒発火
            _countdownTimer.OnTick
                .Subscribe(remainingTime =>
                {
                    //残り時間を保持
                    _remainingTime = remainingTime;
                    //ウェーブタイマーのViewを更新
                    _waveTimerView.UpdateWaveTimer(remainingTime);
                })
                .AddTo(_disposables);
            //タイムオーバー時に発火
            _countdownTimer.OnComplete
                .Subscribe(_ =>
                {
                    //WIP
                    MyLogger.Log("[WaveTimer]タイムオーバー！");
                    _onWaveFinishDetected.OnNext(Unit.Default);
                    ResetWaveTimer();
                })
                .AddTo(_disposables);

            //タイマー開始
            _countdownTimer.StartTimer(timelimit).Forget();
        }


        /// <summary>
        /// ウェーブを停止し初期状態へ戻す
        /// </summary>
        public void ResetWaveTimer()
        {
            //タイマーキャンセル
            if (_isTimerRunning) _countdownTimer.CancelTimer();

            //初期化
            _disposables.Clear();
            _countdownTimer.Dispose();
            _countdownTimer = new CountdownTimer();
        }


        void IDisposable.Dispose()
        {
            //購読解除用
            _disposables.Dispose();
            //タイマー破棄
            _countdownTimer.Dispose();
            //破棄処理
            _onWaveFinishDetected.Dispose();
        }
    }
}