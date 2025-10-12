using ParkingGame.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.Player
{
    /// <summary>
    /// 車を操作するクラス
    /// </summary>
    public class CarController : MonoBehaviour, IPlayerSystem
    {
        //初期座標
        //private Vector3 _startPosition = new Vector3(-10.86f, 4.56f, -11.44f);
        private Vector3 _startPosition = new Vector3(-10.86f, 4.29f, -22.5f);
        private Vector3 _startRotation = new Vector3(0f, 0f, 0f);

        //設定限界値
        private const float _MAX_MOTOR_TORQUE = 600f; //最大トルク
        private const float _MIN_MOTOR_TORQUE = 500f; //最小トルク
        private const float _BRAKE_TORQUE = 599f; //ブレーキトルク
        private const float _MAX_STEERING_ANGLE = 40f; //最大方向転換角度

        //タイヤ
        [SerializeField] private WheelCollider _wheelFL; //左前
        [SerializeField] private WheelCollider _wheelFR; //右前
        [SerializeField] private WheelCollider _wheelBL; //左後ろ
        [SerializeField] private WheelCollider _wheelBR; //右後ろ

        //現在の移動量
        private float _motor;//トルク
        private float _steering;//方向転換角度

        //キャッシュ
        private Rigidbody _rb;

        //入力受付
        public bool CanControll { get; set; } = false;

        public Transform Transform => transform;

        //煙エフェクト
        [SerializeField] private GameObject _smoke;
        private List<GameObject> _smokeObjs = new List<GameObject>();
        private object _currentSmokeObj;

        public void Start()
        {
            //キャッシュを取得
            _rb = GetComponent<Rigidbody>();
        }


        public void FixedUpdate()
        {
            //前進方向の速度を取得
            var towardsVelocity = transform.InverseTransformDirection(_rb.velocity).z;

            //トルクを更新
            if (CanControll && Input.GetKey(KeyCode.W))
            {
                //後退中なら止まるまでブレーキをかける
                if (towardsVelocity >= 0) MoveTowards(_MAX_MOTOR_TORQUE);
                else Brake();
            }
            else if (CanControll && Input.GetKey(KeyCode.S))
            {
                //前進中なら止まるまでブレーキをかける
                if (towardsVelocity <= 0) MoveTowards(-_MAX_MOTOR_TORQUE);
                else Brake();
            }
            else Brake();

            //方向転換角度を等速で更新
            if (CanControll && Input.GetKey(KeyCode.A))
            {
                _steering = Mathf.MoveTowards(_steering, -_MAX_STEERING_ANGLE, 80f * Time.fixedDeltaTime);
            }
            else if (CanControll && Input.GetKey(KeyCode.D))
            {
                _steering = Mathf.MoveTowards(_steering, _MAX_STEERING_ANGLE, 80f * Time.fixedDeltaTime);
            }
            else
            {
                _steering = Mathf.MoveTowards(_steering, 0f, 80f * Time.fixedDeltaTime);
            }
            _wheelFL.steerAngle = _wheelFR.steerAngle = _steering;

            //前輪の見た目を更新
            UpdateWheelView(_wheelFL);
            UpdateWheelView(_wheelFR);
        }


        /// <summary>
        /// WheelColliderの情報からタイヤ見た目を更新
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
            if (targetMotorTorque > 0)_motor = Mathf.Max(_motor, _MIN_MOTOR_TORQUE);
            else _motor = Mathf.Min(_motor, -_MIN_MOTOR_TORQUE);
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


        public void InitPlayerPosition()
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.Euler(_startRotation);
        }


        public void ShowSmokeEffect(Vector3 pos)
        {
            var obj = Instantiate(_smoke, pos, Quaternion.identity);
            _smokeObjs.Add(obj);
        }


        public void DestroySmokeEffect()
        {
            _smokeObjs.ForEach(obj => Destroy(obj));
            _smokeObjs.Clear();
        }
    }
}
