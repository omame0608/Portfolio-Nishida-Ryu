using PerfectBackParkerRev.GameCores.Users;
using PerfectBackParkerRev.Utilities;
using UnityEngine;

namespace PerfectBackParkerRev.Players
{
    /// <summary>
    /// プレイヤーである車を操作するコントローラ
    /// </summary>
    public class PlayerController : MonoBehaviour, IPlayerSystem
    {
        //操作可能かどうか
        public bool CanControll { get; set; } = false;

        //入力受付：trueなら対応した処理が行われ,FixedUpdateの最後にfalseにリセットされる
        public bool AcceleInput { get; set; } = false; //アクセル入力
        public bool BackInput { get; set; } = false; //バック入力
        public bool LeftHandlingInput { get; set; } = false; //左ハンドル入力
        public bool RightHandlingInput { get; set; } = false;　//右ハンドル入力

        //操作用
        public Transform Transform => transform;

        //各種定数
        private const float _MAX_MOTOR_TORQUE = 600f; //最大トルク
        private const float _MIN_MOTOR_TORQUE = 500f; //最小トルク
        private const float _BRAKE_TORQUE = 599f; //ブレーキトルク
        private const float _MAX_STEERING_ANGLE = 40f; //最大前輪角度

        //現在の移動量
        private float _motor;//トルク
        private float _steering;//前輪角度

        //キャッシュ
        private Rigidbody _rb;
        [SerializeField] private WheelCollider _wheelFL; //タイヤ左前
        [SerializeField] private WheelCollider _wheelFR; //タイヤ右前
        [SerializeField] private WheelCollider _wheelBL; //タイヤ左後ろ
        [SerializeField] private WheelCollider _wheelBR; //タイヤ右後ろ

        [Header("当たり判定持ちオブジェクト群")]
        [SerializeField] private GameObject[] _colliderObjects;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            //前進方向の速度を取得
            var towardsVelocity = transform.InverseTransformDirection(_rb.linearVelocity).z;

            //トルクを更新
            if (CanControll && AcceleInput)
            {
                //後退中なら止まるまでブレーキをかける
                if (towardsVelocity >= 0) MoveTowards(_MAX_MOTOR_TORQUE);
                else Brake();
            }
            else if (CanControll && BackInput)
            {
                //前進中なら止まるまでブレーキをかける
                if (towardsVelocity <= 0) MoveTowards(-_MAX_MOTOR_TORQUE);
                else Brake();
            }
            else Brake();

            //方向転換角度を等速で更新
            if (CanControll && LeftHandlingInput)
            {
                _steering = Mathf.MoveTowards(_steering, -_MAX_STEERING_ANGLE, 80f * Time.fixedDeltaTime);
            }
            else if (CanControll && RightHandlingInput)
            {
                _steering = Mathf.MoveTowards(_steering, _MAX_STEERING_ANGLE, 80f * Time.fixedDeltaTime);
            }
            else
            {
                _steering = Mathf.MoveTowards(_steering, 0f, 80f * Time.fixedDeltaTime);
            }
            _wheelFL.steerAngle = _wheelFR.steerAngle = _steering;

            //タイヤの見た目を更新
            UpdateWheelView(_wheelFL);
            UpdateWheelView(_wheelFR);
            UpdateWheelView(_wheelBL);
            UpdateWheelView(_wheelBR);

            //入力受付初期化
            AcceleInput = BackInput = LeftHandlingInput = RightHandlingInput = false;
        }


        /// <summary>
        /// タイヤに加速度的にトルクをかける
        /// </summary>
        /// <param name="targetMotorTorque">目標トルク</param>
        private void MoveTowards(float targetMotorTorque)
        {
            //ブレーキトルクを初期化
            _wheelFL.brakeTorque = _wheelFR.brakeTorque = 0f;

            //トルクを更新
            _motor = Mathf.Lerp(_motor, targetMotorTorque, 0.5f * Time.fixedDeltaTime);
            if (targetMotorTorque > 0)
            {
                _motor = Mathf.Max(_motor, _MIN_MOTOR_TORQUE);
            }
            else
            {
                _motor = Mathf.Min(_motor, -_MIN_MOTOR_TORQUE);
            }
            _wheelFL.motorTorque = _wheelFR.motorTorque = _motor;
        }


        /// <summary>
        /// ブレーキをかける
        /// </summary>
        private void Brake()
        {
            //トルクを初期化しブレーキトルクをかける
            _motor = 0f;
            _wheelFL.brakeTorque = _wheelFR.brakeTorque = _BRAKE_TORQUE;
        }


        /// <summary>
        /// WheelColliderの情報からタイヤの見た目を更新
        /// </summary>
        /// <param name="collider">対象のWheelCollider</param>
        private void UpdateWheelView(WheelCollider collider)
        {
            //タイヤの見た目オブジェクトを取得
            var wheelViewTr = collider.transform.GetChild(0);

            //タイヤのコライダーの情報を取得
            collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

            //見た目に情報を反映
            wheelViewTr.position = position;
            wheelViewTr.rotation = rotation;
        }


        public void PlayStageStartAnimation()
        {
            //アニメーションを再生
            if (TryGetComponent<Animator>(out var animator))
            {
                animator.SetTrigger("StageStart");
            }
            else
            {
                MyLogger.LogError($"[PlayerController]animatorがアタッチされていません");
            }
        }


        public void StopStageStartAnimation()
        {
            //アニメーションを停止・破棄
            if (TryGetComponent<Animator>(out var animator))
            {
                animator.speed = 0f;
                Destroy(animator);
            }
            else
            {
                MyLogger.LogError($"[PlayerController]animatorがアタッチされていません");
            }
        }


        public void ResetPlayerTransform()
        {
            //駐車場入り口上空地点にリセット
            transform.position = new Vector3(-10.82f, 7.8f, -11f);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        public void SwitchRigidbody(bool useGravity)
        {
            //重力を有効化
            _rb.useGravity = useGravity;
            _rb.isKinematic = !useGravity;
        }


        public void SwitchCollision(bool useNormalCollision)
        {
            //ウェーブ失敗時用の当たり判定への  切り替え
            if (!useNormalCollision)
            {
                //Positionを固定
                _rb.constraints = RigidbodyConstraints.FreezePosition;

                //物理判定レイヤーを切り替え
                foreach (var obj in _colliderObjects)
                {
                    obj.layer = LayerMask.NameToLayer("ClearedPlayer");
                }
            }
            //通常通りの当たり判定へ切り替え
            else
            {
                //Positionを解放
                _rb.constraints = RigidbodyConstraints.None;

                //物理判定レイヤーを切り替え
                foreach (var obj in _colliderObjects)
                {
                    obj.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }
}