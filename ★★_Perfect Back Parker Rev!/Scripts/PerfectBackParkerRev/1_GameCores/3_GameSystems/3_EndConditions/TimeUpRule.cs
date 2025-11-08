using System;
using UniRx;
using VContainer;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// 時間切れでウェーブ失敗のルール
    /// </summary>
    public class TimeUpRule : IWaveFinishable, IDisposable
    {
        //検出対象のタイマー
        //再使用時にnewし直すことに注意
        [Inject] private readonly WaveTimer _waveTimer;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        public void AddNotifier(IWaveFinishNotifier notifier)
        {
            throw new InvalidOperationException("[TimeUpRule]TimeUpRuleには動的に通知者を追加できません");
            //動的に追加するときはここへ記述
        }


        //public void RemoveNotifier(IWaveFinishNotifier notifier)


        public void StartObserve(Action<WaveResultType> waveFinishCallback)
        {
            //検出対象のタイマーを購読
            _waveTimer.OnWaveFinishDetected
                .Subscribe(_ =>
                {
                    waveFinishCallback(WaveResultType.Failed);
                })
                .AddTo(_disposables);
        }


        public void StopObserve()
        {
            _disposables.Clear();
        }


        public void Dispose()
        {
            //購読解除用
            _disposables.Dispose();
        }
    }
}