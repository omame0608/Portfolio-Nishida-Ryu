using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using System;
using UniRx;
using UnityEngine;

namespace PerfectBackParkerRev.Notifiers
{
    /// <summary>
    /// 「金のネジ」取得判定
    /// </summary>
    public class GoldenScrewDetector : MonoBehaviour, IGoldenScrewGetNotifier
    {
        //プレイヤーが触れたら発火
        private readonly Subject<Unit> _onGoldenScrewGetDetected = new();
        public IObservable<Unit> OnGoldenScrewGetDetected => _onGoldenScrewGetDetected;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _onGoldenScrewGetDetected.OnNext(Unit.Default);

                //取得後に自身を破棄
                Destroy(gameObject);
            }
        }


        private void OnDestroy()
        {
            _onGoldenScrewGetDetected.Dispose();
        }
    }
}