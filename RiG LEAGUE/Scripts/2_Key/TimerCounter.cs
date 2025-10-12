using System.Collections.Generic;
using UniRx;
using System;
using UnityEngine;

/// <summary>
/// タイマーのカウントダウン処理を行う
/// </summary>
public class TimerCounter
{
    private int _currentTime;
    private List<ITimerCountDownObserver> _timerCountDownObservers = new();
    private List<ITimerObserver> _timerObservers = new();
    private CompositeDisposable _disposables = new();

    public TimerCounter(int limitTime)
    {
        _currentTime = limitTime;
        CreateTimer();
    }

    /// <summary>
    /// カウントダウン通知を受け取るインスタンスを登録する
    /// </summary>
    /// <param name="timerCountDownObserver">登録するインスタンス</param>
    public void AddCountDownObserver(ITimerCountDownObserver timerCountDownObserver) {
        _timerCountDownObservers.Add(timerCountDownObserver);
    }

    /// <summary>
    /// タイムアップ通知を受け取るインスタンスを登録する
    /// </summary>
    /// <param name="timerObserver">登録するインスタンス</param>
    public void AddTimerObserver(ITimerObserver timerObserver) {
        _timerObservers.Add(timerObserver);
    }

    /// <summary>
    /// タイムアップ通知を受け取るインスタンスを削除する
    /// </summary>
    /// <param name="timerObserver">削除するインスタンス</param>
    public void RemoveTimerObserver(ITimerObserver timerObserver) {
        bool couldRemove = _timerObservers.Remove(timerObserver);
        if (!couldRemove) {
            Debug.LogError("指定されたインスタンスは登録されていません");
        }
    }

    /// <summary>
    /// タイマーを作成する
    /// </summary>
    private void CreateTimer() {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .TakeWhile(_ => _currentTime >= 0)
            .Subscribe(time => {
                _currentTime--;

                /* 登録されているカウントダウンアクションを実行する */
                foreach (var observer in _timerCountDownObservers) {
                    observer.OnCountDown(_currentTime);
                }

                if (_currentTime == 0) {
                    /* 登録されているタイムアップアクションを実行する */
                    foreach (var observer in _timerObservers) {
                        observer.OnTimeUp();
                    }

                    Dispose();
                }
            })
            .AddTo(_disposables);
    }

    /// <summary>
    /// 購読を解除する
    /// </summary>
    private void Dispose() {
        _disposables.Dispose();
        _timerCountDownObservers = null;
        _timerObservers = null;
    }
}
