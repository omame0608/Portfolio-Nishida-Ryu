using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// 監視できるDataを所持している
/// </summary>
public interface IDataObservable
{
    //データ本体
    public ReactiveProperty<bool> Data { get; set; }
}
