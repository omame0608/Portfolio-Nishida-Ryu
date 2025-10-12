using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゲーム終了時に表示するパネルの演出を制御するクラス
/// </summary>
/// <author>西田琉</author>
public class Thanks : MonoBehaviour
{
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        DOTween.Sequence()
            .Append(rect.DOScale(new Vector3(1f, 1f, 1f), 1f)
                .SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Yoyo);
    }
}
