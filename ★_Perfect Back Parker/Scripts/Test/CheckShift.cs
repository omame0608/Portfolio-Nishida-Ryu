using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// モッククラス：shiftキーが押されているかチェック
    /// </summary>
    public class CheckShift : MonoBehaviour
    {
        //キーが押されたときのコールバックリスト
        public Action OnShiftClicked;

        void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.LeftShift))
                .Subscribe(_ =>
                {
                    Debug.Log($"シフトキーが押された");
                    OnShiftClicked?.Invoke();
                })
                .AddTo(this);
        }
    }
}