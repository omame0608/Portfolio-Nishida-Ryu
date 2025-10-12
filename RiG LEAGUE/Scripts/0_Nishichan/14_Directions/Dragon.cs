using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 演出用のドラゴンを制御するクラス
/// </summary>
/// <author>西田琉</author>
public class Dragon : MonoBehaviour
{
    private Sequence t;


    void Start()
    {
        t = DOTween.Sequence()
            .Append(transform.DOMoveX(-180f, 15f).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                Destroy(gameObject);
            });
    }
}
