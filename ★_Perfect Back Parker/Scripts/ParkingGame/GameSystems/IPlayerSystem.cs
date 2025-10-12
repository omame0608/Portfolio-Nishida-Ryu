using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// プレイヤーを操作するインターフェース
    /// </summary>
    public interface IPlayerSystem
    {
        /// <summary>
        /// ユーザからの操作を受け付けるかどうか
        /// </summary>
        bool CanControll { get; set; }


        Transform Transform { get; }


        /// <summary>
        /// プレイヤーの位置を初期化する
        /// </summary>
        void InitPlayerPosition();


        /// <summary>
        /// 煙を発生させる
        /// </summary>
        /// <param name="pos"></param>
        void ShowSmokeEffect(Vector3 pos);


        /// <summary>
        /// 煙を削除する
        /// </summary>
        void DestroySmokeEffect();
    }
}