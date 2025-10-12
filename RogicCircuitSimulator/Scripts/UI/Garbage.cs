using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゴミ箱UIクラス
/// </summary>
public class Garbage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //参照
    [SerializeField] private RectTransform _image;

    //初期値取得
    private float _defaultScale;

    
    void Start()
    {
        _defaultScale = _image.localScale.x;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.DOScale(new Vector3(_defaultScale * 1.2f, _defaultScale * 1.2f, _defaultScale * 1.2f), 0.1f);
    }


    //UIのアニメーション各予定
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.DOScale(new Vector3(_defaultScale, _defaultScale, _defaultScale), 0.1f);
    }
}
