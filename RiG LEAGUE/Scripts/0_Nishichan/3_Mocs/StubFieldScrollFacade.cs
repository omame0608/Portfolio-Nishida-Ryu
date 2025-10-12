using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールド窓口のスタブ
/// </summary>
/// <author>西田琉</author>
public class StubFieldScrollFacade : IFieldScrollSystemFacade
{
    public void StartFieldScroll(bool useAcceleration)
    {
        Debug.Log("スクロール開始");
    }

    public void StopFieldScroll(bool useAcceleration)
    {
        Debug.Log("スクロール停止");
    }
}
