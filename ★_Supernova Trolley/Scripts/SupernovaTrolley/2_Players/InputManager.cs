using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SupernovaTrolley.Players
{
    /// <summary>
    /// 入力管理クラス
    /// </summary>

    public class InputManager : MonoBehaviour
    {
        //シングルトン
        public static InputManager Instance { get; private set; }

        //アクションアセット
        [SerializeField] private InputActionAsset _actionAsset;

        //入力受付一覧
        private InputActionMap _actionMap;
        private InputAction _buttonA; //Aボタン
        private InputAction _buttonB; //Bボタン
        private InputAction _buttonX; //Xボタン
        private InputAction _buttonY; //Yボタン
        private InputAction _rightStick; //右スティック
        private InputAction _leftStick; //左スティック
        private InputAction _triggerR; //右トリガー
        private InputAction _triggerL; //左トリガー
        private InputAction _gripR; //右グリップ
        private InputAction _gripL; //左グリップ

        [Title("デバッグ用")]
        [SerializeField, Header("入力に関するログ出力を行うか")] private bool _useLog = true; //ログを使用するかどうか


        private void Awake()
        {
            //シングルトンの設定
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            //アクションマップとアクションの取得
            _actionMap = _actionAsset.FindActionMap("Player");
            _buttonA = _actionMap.FindAction("ButtonA", throwIfNotFound: true);
            _buttonB = _actionMap.FindAction("ButtonB", throwIfNotFound: true);
            _buttonX = _actionMap.FindAction("ButtonX", throwIfNotFound: true);
            _buttonY = _actionMap.FindAction("ButtonY", throwIfNotFound: true);
            _rightStick = _actionMap.FindAction("RightStick", throwIfNotFound: true);
            _leftStick = _actionMap.FindAction("LeftStick", throwIfNotFound: true);
            _triggerR = _actionMap.FindAction("TriggerR", throwIfNotFound: true);
            _triggerL = _actionMap.FindAction("TriggerL", throwIfNotFound: true);
            _gripR = _actionMap.FindAction("GripR", throwIfNotFound: true);
            _gripL = _actionMap.FindAction("GripL", throwIfNotFound: true);

            //アクションの有効化
            _actionMap.Enable();
        }


        private void OnEnable()
        {
            //アクションの有効化
            _actionMap?.Enable();
        }


        private void OnDisable()
        {
            //アクションの無効化
            _actionMap?.Disable();
        }


        //各種入力パラメータを取得
        public static bool ButtonA_OnPress() => Instance._buttonA.WasPressedThisFrame();
        public static bool ButtonB_OnPress() => Instance._buttonB.WasPressedThisFrame();
        public static bool ButtonX_OnPress() => Instance._buttonX.WasPressedThisFrame();
        public static bool ButtonY_OnPress() => Instance._buttonY.WasPressedThisFrame();
        public static Vector2 RightStick_Value() => Instance._rightStick.ReadValue<Vector2>();
        public static Vector2 LeftStick_Value() => Instance._leftStick.ReadValue<Vector2>();
        public static float TriggerR_OnPress() => Instance._triggerR.ReadValue<float>();
        public static float TriggerL_OnPress() => Instance._triggerL.ReadValue<float>();
        public static float GripR_OnPress() => Instance._gripR.ReadValue<float>();
        public static float GripL_OnPress() => Instance._gripL.ReadValue<float>();



        void Update()
        {
            if (!_useLog) return;

            //デバッグ用: Aボタンが押されたらログを出力
            if (ButtonA_OnPress())
            {
                Debug.Log("Aボタンが押されました");
            }
            if (ButtonB_OnPress())
            {
                Debug.Log("Bボタンが押されました");
            }
            if (ButtonX_OnPress())
            {
                Debug.Log("Xボタンが押されました");
            }
            if (ButtonY_OnPress())
            {
                Debug.Log("Yボタンが押されました");
            }
            if (RightStick_Value() != Vector2.zero)
            {
                Debug.Log($"右スティックの値: {RightStick_Value()}");
            }
            if (LeftStick_Value() != Vector2.zero)
            {
                Debug.Log($"左スティックの値: {LeftStick_Value()}");
            }
            if (TriggerR_OnPress() > 0)
            {
                Debug.Log($"右トリガーの値：{TriggerR_OnPress()}");
            }
            if (TriggerL_OnPress() > 0)
            {
                Debug.Log($"左トリガーの値：{TriggerL_OnPress()}");
            }
            if (GripR_OnPress() > 0)
            {
                Debug.Log($"右グリップの値：{GripR_OnPress()}");
            }
            if (GripL_OnPress() > 0)
            {
                Debug.Log($"左グリップの値：{GripL_OnPress()}");
            }
        }
    }
}
