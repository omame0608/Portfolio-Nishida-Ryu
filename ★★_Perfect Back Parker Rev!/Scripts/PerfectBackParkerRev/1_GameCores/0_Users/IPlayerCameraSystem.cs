using System.Transactions;
using UnityEngine;

namespace PerfectBackParkerRev.GameCores.Users
{
    /// <summary>
    /// プレイヤーカメラ操作システムのインターフェース
    /// </summary>
    public interface IPlayerCameraSystem
    {
        //操作可能かどうか
        bool CanControll { get; set; }

        //入力受付
        float UpInput { get; set; } //上入力
        float DownInput { get; set; } //下入力
        float LeftInput { get; set; } //左入力
        float RightInput { get; set; }　//右入力
        float ZoomInput { get; set; } //ズームイン・アウト入力(+でズーム)

        /// <summary>
        /// カメラスフィアのトランスフォームをリセットする
        /// </summary>
        /// <param name="isStartWithZPlusDirection">Zplus方向を向いているかどうか</param>
        void ResetCameraTransform(bool isStartWithZPlusDirection);

        //操作用
        Transform Transform { get; }
    }
}