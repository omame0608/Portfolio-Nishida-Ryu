using PerfectBackParkerRev.Utilities;
using System;
using UniRx;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// ゴール枠に一定時間停止でウェーブ成功のルール
    /// </summary>
    public class GoalRule : IWaveFinishable, IDisposable
    {
        //検出対象のゴール枠
        //動的に対象が変化すること、更新後に再講読しないと適用はされないことに注意
        private IWaveFinishNotifier _goalDetector;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        public void AddNotifier(IWaveFinishNotifier notifier)
        {
            //通知者が登録されていなければ登録
            if (_goalDetector != null)
            { 
                MyLogger.LogError("[GoalRule]既にゴール検出器が登録されています");
                return;
            }
            _goalDetector = notifier;
        }


        //public void RemoveNotifier(IWaveFinishNotifier notifier)


        public void StartObserve(Action<WaveResultType> waveFinishCallback)
        {
            //検出対象のゴール枠を購読
            _goalDetector.OnWaveFinishDetected
                .Subscribe(_ =>
                {
                    waveFinishCallback(WaveResultType.Clear);
                })
                .AddTo(_disposables);
        }


        public void StopObserve()
        {
            //購読解除用
            _disposables.Clear();

            //検出対象を初期化
            _goalDetector = null;
        }


        void IDisposable.Dispose()
        {
            //購読解除用
            _disposables.Dispose();
        }
    }
}