using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 演出用の鳥を制御するクラス
/// </summary>
/// <author>西田琉</author>
public class Bird : MonoBehaviour
{
    private Sequence t;


    // Start is called before the first frame update
    void Start()
    {
        t = DOTween.Sequence()
            .Append(transform.DOMoveZ(100f, 13f).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                Destroy(gameObject);
            });
    }
}
