using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;

namespace PerfectBackParkerRev.Utilities
{
    /// <summary>
    /// カウントダウンタイマー
    /// 使用終了後にDispose()での破棄を推奨
    /// Dispose後の再利用を考えない場合readonlyでの保持を推奨
    /// </summary>
    public class CountdownTimer : IDisposable
    {
        //コールバック関数
        private readonly Subject<int> _onTick = new(); //毎秒発火、残り時間を通知
        private readonly Subject<Unit> _onComplete = new(); //カウントダウン終了を通知
        public IObservable<int> OnTick => _onTick;
        public IObservable<Unit> OnComplete => _onComplete;

        //タイマー管理用
        private bool _isDisposed = false; //破棄済みフラグ
        private CancellationTokenSource _cts; //キャンセル用
        private int _currentTime; //現在の残り時間
        public bool IsRunning => _cts != null;


        /// <summary>
        /// 秒数を指定してカウントダウンを開始する
        /// </summary>
        /// <param name="time">計測する時間(秒)</param>
        /// <param name="ignoreTimeScale">UnityのtimeScaleを無視するか デフォルトでゲーム時間準拠</param>
        public async UniTask StartTimer(int time, bool ignoreTimeScale = false)
        {
            //バリデーション
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CountdownTimer), "既に破棄されたタイマーです");
            if (time <= 0)
                throw new ArgumentOutOfRangeException(nameof(time), "タイマー時間は1秒以上を指定してください");
            if (IsRunning)
                throw new InvalidOperationException("既に動作中のタイマーです");

            //カウントダウン開始
            _cts = new CancellationTokenSource();
            _currentTime = time;
            try
            {
                //0秒でもonTickが発火するようにループ
                while (true)
                {
                    //毎秒コールバック
                    MyLogger.Log($"[Timer]残り時間: {_currentTime}秒", false);
                    _onTick.OnNext(_currentTime);

                    //残り時間更新
                    _currentTime--;

                    //カウントダウン終了でなければ1秒待機
                    if (_currentTime < 0) break;
                    await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: ignoreTimeScale, cancellationToken: _cts.Token);
                }

                //カウントダウン完了コールバック
                MyLogger.Log("[Timer]カウントダウン完了", false);
                _onComplete.OnNext(Unit.Default);
            }
            catch (OperationCanceledException)
            {
                //キャンセル時は何もしない
                MyLogger.Log("[Timer]カウントダウンキャンセル", false);
            }
            finally
            {
                //タイマー終了処理
                _cts?.Dispose();
                _cts = null;
            }
        }


        /// <summary>
        /// 動作中のタイマーをキャンセルする
        /// </summary>
        public void CancelTimer()
        {
            //バリデーション
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CountdownTimer), "既に破棄されたタイマーです");
            if (!IsRunning)
                throw new InvalidOperationException("動作中のタイマーではありません");

            //キャンセル実行
            _cts?.Cancel();
        }


        /// <summary>
        /// タイマーを破棄する
        /// 安全にタイマーの使用を終了し、以降このインスタンスは使用できない状態となる
        /// </summary>
        public void Dispose()
        {
            //バリデーション
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(CountdownTimer), "既に破棄されたタイマーです");

            //動作中であればキャンセル
            if (IsRunning) CancelTimer();

            //破棄処理
            _isDisposed = true;
            _onTick.Dispose();
            _onComplete.Dispose();
        }
    }
}