using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Timerクラスを監視しタイムアップに対するアクションができるオブジェクト
/// </summary>
/// <author>西田琉</author>
public interface ITimerObserver
{
    /// <summary>
    /// タイムアップに対するアクション
    /// </summary>
    void OnTimeUp();
}