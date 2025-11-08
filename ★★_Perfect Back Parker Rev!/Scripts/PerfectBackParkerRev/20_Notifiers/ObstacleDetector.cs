using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using System;
using UniRx;
using UnityEngine;

namespace PerfectBackParkerRev.Notifiers
{
    /// <summary>
    /// 障害物の当たり判定
    /// </summary>
    public class ObstacleDetector : MonoBehaviour, IWaveFinishNotifier
    {
        //プレイヤーと衝突で発火
        private readonly Subject<Unit> _onWaveFinishDetected = new();
        public IObservable<Unit> OnWaveFinishDetected => _onWaveFinishDetected;


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                _onWaveFinishDetected.OnNext(Unit.Default);
            }
        }


        private void OnDestroy()
        {
            _onWaveFinishDetected.Dispose();
        }
    }
}