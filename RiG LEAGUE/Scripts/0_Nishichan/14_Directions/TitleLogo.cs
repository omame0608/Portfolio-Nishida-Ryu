using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// タイトルロゴのアニメーションを制御するクラス
/// </summary>
/// <author>西田琉</author>
public class TitleLogo : MonoBehaviour
{
    private Sequence _sequence;

    void Start()
    {
        _sequence = DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0f, 360f, 0f), 3f)
                .SetRelative()
                .SetEase(Ease.InOutExpo))
            .AppendInterval(7f)
            .SetLoops(-1);
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
