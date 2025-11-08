using UnityEngine;

namespace PerfectBackParkerRev.GameCores.Users
{
    /// <summary>
    /// プレイヤー操作システムのインターフェース
    /// </summary>
    public interface IPlayerSystem
    {
        //操作可能かどうか
        bool CanControll { get; set; }

        //入力受付
        bool AcceleInput { get; set; } //アクセル入力
        bool BackInput { get; set; } //バック入力
        bool LeftHandlingInput { get; set; } //左ハンドル入力
        bool RightHandlingInput { get; set; }　//右ハンドル入力

        //操作用
        Transform Transform { get; }

        /// <summary>
        /// STAGEスタート時のアニメーションを再生する
        /// </summary>
        void PlayStageStartAnimation();

        /// <summary>
        /// STAGEスタート時のアニメーションを停止する
        /// </summary>
        void StopStageStartAnimation();

        /// <summary>
        /// プレイヤーのTransformを初期位置にリセットする
        /// </summary>
        void ResetPlayerTransform();

        /// <summary>
        /// 重力を適用する
        /// </summary>
        /// <param name="usePhysics">物理演算を利用する</param>
        void SwitchRigidbody(bool usePhysics);


        /// <summary>
        /// ウェーブ失敗時にコリジョンの当たり判定を切り替える
        /// </summary>
        /// <param name="useNormalCollision">通常通りの当たり判定を利用するか</param>
        void SwitchCollision(bool useNormalCollision);
    }
}