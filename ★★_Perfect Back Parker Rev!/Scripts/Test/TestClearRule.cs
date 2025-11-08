using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using System;
using System.Collections.Generic;
using UniRx;
using VContainer;

namespace Test
{
    /// <summary>
    /// テスト用：Spaceでクリア通知を発行
    /// </summary>
    public class TestClearRule : IWaveFinishable, IDisposable
    {
        //監視対象
        [Inject, Key("TestClearRule")] private readonly IWaveFinishNotifier _testClearNotifier;

        //購読解除用
        private readonly CompositeDisposable _disposables = new();

        public List<IWaveFinishNotifier> NotifierList => throw new NotImplementedException();

        public void AddNotifier(IWaveFinishNotifier notifier)
        {
            throw new NotImplementedException();
        }

        public void RemoveNotifier(IWaveFinishNotifier notifier)
        {
            throw new NotImplementedException();
        }

        public void StartObserve(Action<WaveResultType> waveFinishCallback)
        {
            _disposables.Clear();

            //クリア通知を購読
            _testClearNotifier.OnWaveFinishDetected
                .Subscribe(_ => waveFinishCallback(WaveResultType.Clear))
                .AddTo(_disposables);
        }


        public void StopObserve()
        {
            //購読解除
            _disposables.Clear();
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}