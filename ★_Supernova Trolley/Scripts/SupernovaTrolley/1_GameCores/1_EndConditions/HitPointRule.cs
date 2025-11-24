using SupernovaTrolley.GameCores.Statuses;
using System;
using UniRx;
using VContainer;

namespace SupernovaTrolley.GameCores.EndConditions
{
    /// <summary>
    /// HPが0になったらゲームオーバーとするルール
    /// </summary>
    public class HitPointRule : IGameFinishable, IDisposable
    {
        //システム
        [Inject] private readonly HitPointStatus _hitPointStatus;

        //購読解除用
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void StartObserve(Action<GameResultType> gameFinishCallback)
        {
            _hitPointStatus.OnHitPointZero
                .Subscribe(_ =>
                {
                    gameFinishCallback(GameResultType.GameOver);
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