using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.GameSystems
{
    /// <summary>
    /// カメラを操作するインターフェース
    /// </summary>
    public interface ICameraSystem
    {
        /// <summary>
        /// ユーザからの操作を受け付けるかどうか
        /// </summary>
        bool CanControll { get; set; }

        Transform Transform { get; }

        /// <summary>
        /// カメラの位置を初期化する
        /// </summary>
        void InitCameraPosition();
    }
}