using System;
using UniRx;
using Utilities;

namespace SupernovaTrolley.GameCores.Statuses
{
    /// <summary>
    /// ヒットポイントを管理するステータスクラス
    /// </summary>
    public class HitPointStatus : IDisposable
    {
        //ステータス
        private const int _MAX_HP = 3; //最大ヒットポイント
        private int _currentHP; //現在のヒットポイント

        //HP0通知
        private Subject<Unit> _onHitPointZero;
        public IObservable<Unit> OnHitPointZero => _onHitPointZero;


        /// <summary>
        /// HPステータスの初期化
        /// </summary>
        public void InitStatus()
        {
            _currentHP = _MAX_HP;
            _onHitPointZero = new Subject<Unit>();
        }


        /// <summary>
        /// HPを1減らす
        /// HPが0になったら通知を送る
        /// </summary>
        public void DecreaseHP()
        {
            if (_currentHP > 0)
            {
                _currentHP--;
                MyLogger.Log($"ダメージ!!残りHP:{_currentHP}");

                if (_currentHP == 0)
                {
                    _onHitPointZero?.OnNext(Unit.Default);
                }
            }
            else 
            {
                MyLogger.LogError($"HPが0でダメージを受けています");
            }
        }


        void IDisposable.Dispose()
        {
            _onHitPointZero?.Dispose();
        }
    }
}