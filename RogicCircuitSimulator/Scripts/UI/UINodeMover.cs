using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UIElements;
using VContainer;

/// <summary>
/// UIのNodeプレハブの移動やNode本体の生成を行うクラス
/// </summary>
public class UINodeMover : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler
{
    //生成するNode
    [SerializeField] private INodeFactory.NodeType _nodeType;

    //参照
    [Inject] INodeFactory _nodeFactory;

    //操作用
    private Vector3 _basePos; //初期位置
    private Vector3 _offset; //中心からクリックした場所までの差

    //キャッシュ
    private RectTransform _rectTransform;
    

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _basePos = _rectTransform.localPosition;
    }


    public void OnPointerClick(PointerEventData eventData)
    { 
    }


    public void OnDrag(PointerEventData eventData)
    {
        //移動後の座標を作成
        Vector3 nextPos = Input.mousePosition;

        //対象を移動
        _rectTransform.position = nextPos;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //初期位置に戻す
        _rectTransform.localPosition = _basePos;

        //Nodeを生成する
        _nodeFactory.Create(_nodeType, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
