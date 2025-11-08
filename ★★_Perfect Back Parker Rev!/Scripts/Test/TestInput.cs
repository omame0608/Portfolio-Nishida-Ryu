using PerfectBackParkerRev.Players;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用の入力クラス
    /// </summary>
    public class TestInput : MonoBehaviour
    {
        //システム
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerCameraController _playerCameraController;


        private void Start()
        {
            _playerController.CanControll = true;
            _playerCameraController.CanControll = true;
        }


        private void Update()
        {
            //移動
            if (Input.GetKey(KeyCode.W)) _playerController.AcceleInput = true;
            if (Input.GetKey(KeyCode.A)) _playerController.LeftHandlingInput = true;
            if (Input.GetKey(KeyCode.S)) _playerController.BackInput = true;
            if (Input.GetKey(KeyCode.D)) _playerController.RightHandlingInput = true;

            //カメラ：キーボード
            if (Input.GetKey(KeyCode.UpArrow)) _playerCameraController.UpInput = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) _playerCameraController.LeftInput = 1;
            if (Input.GetKey(KeyCode.DownArrow)) _playerCameraController.DownInput = 1;
            if (Input.GetKey(KeyCode.RightArrow)) _playerCameraController.RightInput = 1;

            //カメラ：マウス
            if (Input.GetMouseButton(2) || Input.GetMouseButton(1) || Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X") * 5;
                float mouseY = Input.GetAxis("Mouse Y") * 5;

                if (mouseX > 0) _playerCameraController.LeftInput = mouseX;
                if (mouseX < 0) _playerCameraController.RightInput = -mouseX;
                if (mouseY < 0) _playerCameraController.UpInput = -mouseY;
                if (mouseY > 0) _playerCameraController.DownInput = mouseY;
            }

            //カメラズーム
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _playerCameraController.ZoomInput = scroll;
        }
    }
}