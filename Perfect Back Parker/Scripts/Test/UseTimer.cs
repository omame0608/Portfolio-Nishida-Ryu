using Cysharp.Threading.Tasks;
using ParkingGame.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Test
{
    public class UseTimer : MonoBehaviour
    {
        private CountdownTimer _timer;

        // Start is called before the first frame update
        void Start()
        {
            _timer = new CountdownTimer();

            _timer.OnTick.Subscribe(time => Debug.Log($"Tick"));
            _timer.OnComplete.Subscribe(time => Debug.Log($"Complete"));

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.P))
                .Subscribe(_ => {
                    _timer.StartTimer(10).Forget();
                });

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(_ => {
                    _timer.CancelTimer();
                });
        }
    }
}