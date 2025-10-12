using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer.Unity;
using ParkingGame.GameSystems;
using System;
using Cysharp.Threading.Tasks;

namespace Test
{
    /// <summary>
    /// モッククラス：スペースキーが押されているかチェック
    /// </summary>
    public class CheckSpace : MonoBehaviour
    {
        //キーが押されたときのコールバックリスト
        public Action OnSpaceClicked;

        void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ =>
                {
                    Debug.Log($"スペースキーが押された");
                    OnSpaceClicked?.Invoke();
                })
                .AddTo(this);
        }
    }
}
