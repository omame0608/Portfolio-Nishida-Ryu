using PerfectBackParkerRev.GameCores.Users;
using UnityEngine;

namespace PerfectBackParkerRev.Directions.Openings
{
    /// <summary>
    /// 演出用：オープニングシーンでプレイヤーを動かすクラス
    /// </summary>
    [RequireComponent(typeof(IPlayerSystem))]
    public class OpeningPlayerMover : MonoBehaviour
    {
        //操作対象
        private IPlayerSystem _playerController;


        private void Start()
        {
            //プレイヤーオブジェクトを取得して操作可能にする
            _playerController = GetComponent<IPlayerSystem>();
            _playerController.CanControll = true;
        }


        private void Update()
        {
            //常にアクセルを踏んでいる状態にしてタイヤを回す
            _playerController.AcceleInput = true;
        }
    }
}