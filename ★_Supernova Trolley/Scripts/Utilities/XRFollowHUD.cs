using Alchemy.Inspector;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// XRデバイスのHUDを追従させるコンポーネント
    /// </summary>
    public class XRFollowHUD : MonoBehaviour
    {
        [Title("追従対象のオブジェクト")]
        [SerializeField] private Transform _target;

        [Title("各種パラメータ調整")]
        [SerializeField] private float _distance = 2f;
        [SerializeField] private float _height = 1.4f;
        [SerializeField] private float _followMoveSpeed = 3f;


        private void LateUpdate()
        {
            //HUDの位置・回転を更新
            UpdatePosition();
            UpdateRotation();
        }


        /// <summary>
        /// HUDの位置を更新
        /// </summary>
        private void UpdatePosition()
        {
            var targetPosition = GetTargetPosition();
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _followMoveSpeed);
        }


        /// <summary>
        /// ターゲットの位置を取得
        /// </summary>
        /// <returns>高さを補正したターゲットの座標</returns>
        private Vector3 GetTargetPosition()
        {
            var pos = _target.position + _target.forward * _distance;
            pos.y = _height;
            return pos;
        }


        /// <summary>
        /// HUDの回転を更新
        /// </summary>
        private void UpdateRotation()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _target.position);
        }
    }
}