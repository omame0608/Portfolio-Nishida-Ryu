using PerfectBackParkerRev.GameCores.Users;
using UnityEngine;

namespace PerfectBackParkerRev.Players
{
    /// <summary>
    /// プレイヤーである車のカメラを操作するコントローラ
    /// </summary>
    public class PlayerCameraController : MonoBehaviour, IPlayerCameraSystem
    {
        //操作可能かどうか
        public bool CanControll { get; set; } = false;

        //入力受付：入力された変位量に対応した処理が行われ,Updateの最後に0にリセットされる
        public float UpInput { get; set; } = 0f; //上入力
        public float DownInput { get; set; } = 0f; //下入力
        public float LeftInput { get; set; } = 0f; //左入力
        public float RightInput { get; set; } = 0f;　//右入力
        public float ZoomInput { get; set; } = 0f; //ズームイン・アウト入力(+でズーム)

        //各種定数
        private const float _MIN_ROTATION_X = 5f; //縦回転量下限値
        private const float _MAX_ROTATION_X = 90f; //縦回転量上限値
#if UNITY_WEBGL
        private const float _CAMERA_SPEED = 45f; //カメラ回転速度
#else
        private const float _CAMERA_SPEED = 90f; //カメラ回転速度
#endif
        private const float _MIN_DISTANCE = 5f; //カメラ距離下限値
        private const float _MAX_DISTANCE = 35f; //カメラ距離上限値
        private const float _ZOOM_SPEED = 10f; //ズーム速度

        //現在の回転座標・カメラ距離
        private float _rotationX; //縦回転量
        private float _rotationY; //横回転量
        private float _cameraDistance; //カメラ距離(カメラスフィアのScale)

        //操作用
        public Transform Transform => _cameraSphere;

        //キャッシュ
        [SerializeField] private Transform _cameraSphere;


        private void Start()
        {
            //回転座標の初期値設定
            _rotationX = 40f;
            _rotationY = 0f;
            _cameraDistance = 25f;
        }


        private void Update()
        {
            if (!CanControll) return;

            //カメラを回転量を更新
            if (LeftInput > 0)
            {
                _rotationY += LeftInput * _CAMERA_SPEED * Time.deltaTime;
            }
            if (RightInput > 0)
            {
                _rotationY -= RightInput * _CAMERA_SPEED * Time.deltaTime;
            }
            if (UpInput > 0)
            {
                _rotationX += UpInput * _CAMERA_SPEED * Time.deltaTime;
                _rotationX = Mathf.Min(_rotationX, _MAX_ROTATION_X);
            }
            if (DownInput > 0)
            {
                _rotationX -= DownInput * _CAMERA_SPEED * Time.deltaTime;
                _rotationX = Mathf.Max(_rotationX, _MIN_ROTATION_X);
            }
            _cameraSphere.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);

            //カメラの距離を更新
            _cameraDistance -= ZoomInput * _ZOOM_SPEED * Time.deltaTime * 100;
            _cameraDistance = Mathf.Clamp(_cameraDistance, _MIN_DISTANCE, _MAX_DISTANCE);
            _cameraSphere.localScale = Vector3.one * _cameraDistance;

            //入力受付初期化
            UpInput = DownInput = LeftInput = RightInput = ZoomInput = 0f;
        }


        public void ResetCameraTransform(bool isStartWithZPlusDirection)
        {
            //回転座標・カメラ距離を初期化
            _rotationX = 40f;
            _rotationY = isStartWithZPlusDirection ? 0f : 180f;
            _cameraDistance = 25f;

            //更新
            _cameraSphere.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
            _cameraSphere.localScale = Vector3.one * _cameraDistance;
        }
    }
}