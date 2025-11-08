using System;
using System.Collections.Generic;
using UniRx;

namespace PerfectBackParkerRev.GameCores.GameSystems.EndConditions
{
    /// <summary>
    /// 障害物との衝突でウェーブ失敗のルール
    /// </summary>
    public class ObstacleCollisionRule : IWaveFinishable, IDisposable
    {
        //検出対象の障害物
        //動的に対象が変化すること、増減後再購読しないと適用はされないことに注意
        private List<IWaveFinishNotifier> _obstaclesList = new();

        //購読解除用
        private readonly CompositeDisposable _disposables = new();


        public void AddNotifier(IWaveFinishNotifier notifier) => _obstaclesList.Add(notifier);


        //public void RemoveNotifier(IWaveFinishNotifier notifier) => _obstaclesList.Remove(notifier);


        public void StartObserve(Action<WaveResultType> waveFinishCallback)
        {
            //検出対象の障害物全ての衝突発火を購読
            foreach (var obstacle in _obstaclesList)
            {
                obstacle.OnWaveFinishDetected
                    .Subscribe(_ =>
                    {
                        waveFinishCallback(WaveResultType.Failed);
                    })
                    .AddTo(_disposables);
            }
        }


        public void StopObserve()
        {
            //購読解除用
            _disposables.Clear();

            //検出対象リストを初期化
            _obstaclesList = new List<IWaveFinishNotifier>();
        }


        void IDisposable.Dispose()
        {
            //購読解除用
            _disposables.Dispose();
        }
    }
}