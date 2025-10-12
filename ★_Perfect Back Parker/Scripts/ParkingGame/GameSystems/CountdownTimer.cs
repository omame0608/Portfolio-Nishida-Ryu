using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UniRx;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// 指定秒数カウントダウンを行うタイマー
    /// </summary>
    public class CountdownTimer
    {
        //タイマー処理用
        private int _currentTime; //現在の残り時間
        public bool IsRunning { get; private set; } //タイマーが動作しているかどうか
        private CancellationTokenSource _cts;

        //コールバック関数
        private Subject<int> _onTick = new Subject<int>(); //毎秒呼び出されるコールバック
        private Subject<Unit> _onComplete = new Subject<Unit>(); //カウント終了時のコールバック

        public IObservable<int> OnTick => _onTick;
        public IObservable<Unit> OnComplete => _onComplete;


        /// <summary>
        /// タイマーを使用する
        /// </summary>
        /// <param name="countTime">計測する時間（秒）</param>
        /// <returns></returns>
        public async UniTask StartTimer(int countTime)
        {
            if (IsRunning)
            {
                Debug.LogError($"[Timer]このタイマーは使用中です");
                return;
            }
            IsRunning = true;

            if (countTime <= 0)
            {
                Debug.LogError($"[Timer]タイマーエラー:指定秒数が不正値です");
                return;
            }
            _currentTime = countTime;

            _cts = new CancellationTokenSource();

            //カウントダウンを開始する
            try
            {
                //1秒ずつカウントする
                while (true)
                {
                    //毎秒コールバック
                    Debug.Log($"[Timer]残り：{_currentTime}");
                    _onTick.OnNext(_currentTime);

                    //残り時間更新
                    _currentTime--;

                    //0秒で無ければ1秒待機
                    if (_currentTime < 0) break;
                    await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: _cts.Token);
                }

                //タイムアップコールバック
                Debug.Log($"[Timer]タイムアップ");
                _onComplete.OnNext(Unit.Default);
            }
            catch (OperationCanceledException)
            {
                Debug.Log($"[Timer]タイマーがキャンセルされました");
            }
            finally
            {
                //タイマー終了時処理
                IsRunning = false;
                _cts.Dispose();
                _cts = null;
            }
        }


        /// <summary>
        /// タイマーを停止する
        /// </summary>
        public void CancelTimer()
        {
            if (!IsRunning)
            {
                Debug.LogError($"指定されたタイマーは動作していません");
                return;
            }

            _cts?.Cancel();
        }


        public void Dispose()
        {
            _onTick.Dispose();
            _onComplete.Dispose();
        }
    }
}