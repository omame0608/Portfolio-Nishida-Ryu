using ParkingGame.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.Player
{
    /// <summary>
    /// カメラを操作するクラス
    /// </summary>
    public class CameraController : MonoBehaviour, ICameraSystem
    {
        //カメラスフィア
        [SerializeField] private Transform _cameraSphere;

        //カメラ設定限界値
        //private const float _MIN_ZOOM = 10f; //ズーム最小値
        //private const float _MAX_ZOOM = 30f; //ズーム最大値
        private const float _MIN_ROTATION_X = -85f; //縦回転量下限値
        private const float _MAX_ROTATION_X = 0f;   //縦回転量上限値

        //定数
        private const float _cameraSpeed = 90f;

        //現在の回転座標
        private float _rotationX;
        private float _rotationY;

        //入力受付
        public bool CanControll { get; set; } = false;

        public Transform Transform => _cameraSphere;


        public void Start()
        {
            //回転座標の初期値設定
            _rotationX = -45f;
            _rotationY = 45f;
        }


        public void Update()
        {
            if (!CanControll) return;

            //入力受付
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rotationY += _cameraSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rotationY -= _cameraSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _rotationX += _cameraSpeed * Time.deltaTime;
                _rotationX = Mathf.Min(_rotationX, _MAX_ROTATION_X);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                _rotationX -= _cameraSpeed * Time.deltaTime;
                _rotationX = Mathf.Max(_rotationX, _MIN_ROTATION_X);
            }

            //カメラを移動
            _cameraSphere.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        }


        public void InitCameraPosition()
        {
            Debug.Log($"カメラ向きを初期化");
            _rotationX = -45f;
            _rotationY = 45f;
            _cameraSphere.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        }
    }
}
