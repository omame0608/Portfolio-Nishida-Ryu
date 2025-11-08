using PerfectBackParkerRev.GameCores.GameSystems;
using PerfectBackParkerRev.GameCores.Users;
using PerfectBackParkerRev.Utilities;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// ゲーム画面の入力受付
    /// </summary>
    public class GameSceneInput : MonoBehaviour
    {
        //ハンドラ
        [Inject] private readonly IGameInputHandler _gameInputHandler;
        [Inject] private readonly IPlayerSystem _playerSystem;
        [Inject] private readonly IPlayerCameraSystem _cameraSystem;

        //UI要素
        [SerializeField] private ClickableButton _pauseButton; //ポーズボタン


        private void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => {
                    //----------プレイヤー操作----------
                    if (_playerSystem.CanControll)
                    {
                        //移動
                        _playerSystem.AcceleInput = Input.GetKey(KeyCode.W);
                        _playerSystem.LeftHandlingInput = Input.GetKey(KeyCode.A);
                        _playerSystem.BackInput = Input.GetKey(KeyCode.S);
                        _playerSystem.RightHandlingInput = Input.GetKey(KeyCode.D);
                    }

                    //----------カメラ操作----------
                    if (_cameraSystem.CanControll)
                    {
                        //カメラ：キーボード
                        _cameraSystem.UpInput = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
                        _cameraSystem.LeftInput = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
                        _cameraSystem.DownInput = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
                        _cameraSystem.RightInput = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
                        //カメラ：マウス
                        if (Input.GetMouseButton(0)
                            || Input.GetMouseButton(1)
                            || Input.GetMouseButton(2))
                        {
                            float mouseX = Input.GetAxis("Mouse X") * 5;
                            float mouseY = Input.GetAxis("Mouse Y") * 5;
                            if (mouseX > 0) _cameraSystem.LeftInput = mouseX;
                            if (mouseX < 0) _cameraSystem.RightInput = -mouseX;
                            if (mouseY < 0) _cameraSystem.UpInput = -mouseY;
                            if (mouseY > 0) _cameraSystem.DownInput = mouseY;
                        }
                        //カメラズーム
                        float scroll = Input.GetAxis("Mouse ScrollWheel");
                        _cameraSystem.ZoomInput = scroll;
                    }

                    //----------Enterキーまたはクリック----------
                    if (Input.GetKeyDown(KeyCode.Return)
                        || Input.GetMouseButtonDown(0)
                        || Input.GetMouseButtonDown(1)
                        || Input.GetMouseButtonDown(2))
                    {
                        _gameInputHandler.OnInputEvent(GameSceneInputType.Return);
                    }
                })
                .AddTo(this);

            //----------ポーズ----------
            //UIボタン操作
            _pauseButton.OnClickSubject.Subscribe(_ =>
            {
                _gameInputHandler.OnInputEvent(GameSceneInputType.Pause);
            })
            .AddTo(this);
        }
    }
}