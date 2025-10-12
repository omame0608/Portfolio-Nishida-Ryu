using ParkingGame.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ParkingGame.Field
{
    /// <summary>
    /// プレイヤーとの衝突を検知するクラス
    /// </summary>
    public class CollisionDetector : MonoBehaviour
    {
        //プレイヤー衝突時のコールバックリスト
        public Subject<Unit> OnCollision = new Subject<Unit>();


        public void OnCollisionEnter(Collision collision)
        {
            //プレイヤーが衝突したらコールバック関数を呼び出す
            if (collision.transform.CompareTag("Player"))
            {
                OnCollision.OnNext(Unit.Default);

                // 衝突地点を取得
                Vector3 contactPoint = collision.contacts[0].point;

                // 煙を発生させる
                collision.gameObject.GetComponent<IPlayerSystem>().ShowSmokeEffect(contactPoint);
            }
        }

        private void OnDestroy()
        {
            OnCollision?.Dispose();
        }
    }
}