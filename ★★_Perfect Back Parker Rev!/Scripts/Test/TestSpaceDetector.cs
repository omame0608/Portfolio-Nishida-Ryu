using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用：スペースキー検出
    /// </summary>
    public class TestSpaceDetector : MonoBehaviour, IWaveFinishNotifier
    {
        //インターフェース実装
        private readonly Subject<Unit> _onWaveFinishDetected = new Subject<Unit>();
        public IObservable<Unit> OnWaveFinishDetected => _onWaveFinishDetected;


        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ => _onWaveFinishDetected.OnNext(Unit.Default))
                .AddTo(this);
        }
    }
}