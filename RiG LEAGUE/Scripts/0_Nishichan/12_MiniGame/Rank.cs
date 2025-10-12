using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

/// <summary>
/// 共通のランク表示の動き
/// </summary>
/// <author>西田琉</author>
public class Rank : MonoBehaviour
{
    void Start()
    {
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0f, 360f, 0f), 3f)
                .SetRelative()
                .SetEase(Ease.InOutExpo))
            .AppendInterval(5f)
            .SetLoops(-1);
    }
}
