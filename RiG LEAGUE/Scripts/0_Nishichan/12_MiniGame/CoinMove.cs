using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 流れてくるコインの動きを制御するクラス
/// </summary>
/// <author>西田琉</author>
public class CoinMove : MonoBehaviour
{
    //流れるスピード
    private const float _SPEED = 6.5f;

    //アニメーション
    private Tweener _rotate;
    private Tweener _move;


    void Start()
    {
        //回転
        _rotate = transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 1f)
            .SetEase(Ease.Linear)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Incremental);

        //移動
        _move = transform.DOLocalMove(new Vector3(0f, 0f, -_SPEED), 1f)
            .SetEase(Ease.Linear)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Incremental);
    }


    public void OnTriggerEnter(Collider other)
    {
        //プレイヤーに当たったらポイントを与えて自身を削除する
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            TrolleyController trolley = other.transform.root.GetComponent<TrolleyController>();
            trolley.GetCoin();
            _rotate.Kill(true);
            _move.Kill(true);
            Destroy(gameObject);
        }

        //ストッパーに当たったら自身を削除する
        if (other.gameObject.CompareTag("Stopper"))
        {
            _rotate.Kill(true);
            _move.Kill(true);
            Destroy(gameObject);
        }
    }
}
