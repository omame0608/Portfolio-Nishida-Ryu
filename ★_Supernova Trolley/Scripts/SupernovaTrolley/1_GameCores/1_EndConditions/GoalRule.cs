using SupernovaTrolley.GameCores.Statuses;
using System;
using UniRx;
using VContainer;

namespace SupernovaTrolley.GameCores.EndConditions
{
    /// <summary>
    /// 60秒耐えるとゲームクリアとするルール
    /// </summary>
    public class GoalRule : IGameFinishable, IDisposable
    {
        //システム
        [Inject] private readonly TimeStatus _timeStatus;

        //購読解除用
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void StartObserve(Action<GameResultType> gameFinishCallback)
        {
            
            _timeStatus.OnTimerZero
                .Subscribe(_ =>
                {
                    gameFinishCallback(GameResultType.Clear);
                })
                .AddTo(_disposables);
        }


        public void StopObserve()
        {
            _disposables.Clear();
        }


        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}