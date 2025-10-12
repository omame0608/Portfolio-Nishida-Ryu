using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

/// <summary>
/// Nodesタブを開閉するボタン
/// </summary>
public class NodesTubButton : MonoBehaviour, IPointerClickHandler
{
    //フィールド
    private bool _isOpen;

    //参照
    [SerializeField] private RectTransform _tab; //タブ本体
    [SerializeField] private RectTransform _arrow; //矢印


    /// <summary>
    /// クリックされたらNodesタブを開閉する
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        //左クリックのみ対応
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //タブが閉じていたら
            if (!_isOpen)
            {
                _tab.DOAnchorPosX(100f, 0.5f).SetEase(Ease.OutExpo); //開ける
                _arrow.Rotate(new Vector3(0f, 0f, -180f)); //矢印の向きを変える
                _isOpen = true;
            }
            //タブが開いていたら
            else
            {
                _tab.DOAnchorPosX(-100f, 0.5f).SetEase(Ease.OutExpo); //閉める
                _arrow.Rotate(new Vector3(0f, 0f, -180f)); //矢印の向きを変える
                _isOpen = false;
            }
        }
    }
}
