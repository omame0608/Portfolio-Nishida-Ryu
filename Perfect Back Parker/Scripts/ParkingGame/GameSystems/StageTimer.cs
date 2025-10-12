using Cysharp.Threading.Tasks;
using ParkingGame.GameSystems.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// ステージの制限時間を管理するクラス
    /// </summary>
    public class StageTimer
    {
        //View
        [Inject] private IStageTimerView _timerView;

        //タイマー
        private CountdownTimer _timer;

        //タイムアップ時のコールバック関数
        private Subject<Unit> _onTimeUp = new Subject<Unit>();
        public IObservable<Unit> OnTimeUp => _onTimeUp;


        /// <summary>
        /// ステージのカウントダウンを開始する
        /// </summary>
        /// <param name="timelimit">制限時間</param>
        public void StartStageTimer(int timelimit)
        {
            //タイマーの使用準備
            _timer = new CountdownTimer();
            _timer.OnTick.Subscribe(time => _timerView.UpdateTimerView(time));
            _timer.OnComplete.Subscribe(_ => _onTimeUp.OnNext(Unit.Default));

            //カウントダウン開始
            if (!_timer.IsRunning) _timer.StartTimer(timelimit).Forget();
        }


        /// <summary>
        /// ステージタイマーを停止する
        /// </summary>
        public void StopStageTimer()
        {
            if (_timer.IsRunning) _timer.CancelTimer();
            _timer.Dispose();
        }
    }
}