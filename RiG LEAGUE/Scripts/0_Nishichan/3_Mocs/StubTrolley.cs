using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// トロッコ窓口のスタブ
/// </summary>
/// <author>西田琉</author>
public class StubTrolley : MonoBehaviour, ITrolleySystemFacade
{
    public void DamageTrolley()
    {
        Debug.Log($"トロッコView:ダメージ");
    }

    public void FadeoutTrolley()
    {
        Debug.Log($"トロッコView:フェードアウト");
    }

    public void JumpTrolley()
    {
        Debug.Log($"トロッコView:ジャンプ");
    }

    public void MoveTrolley()
    {
        Debug.Log($"トロッコView:動き始めた");
    }

    public void StopTrolley()
    {
        Debug.Log($"トロッコView:停止");
    }
}
