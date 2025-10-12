using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParkingGame.GameSystems;
using VContainer;
using VContainer.Unity;

namespace ParkingGame
{
    /// <summary>
    /// ゲーム全体の処理開始地点
    /// </summary>
    public class ParkingGameEntryPoint : IInitializable
    {
        //参照
        [Inject] private readonly GameSystem _gameSystem;


        /// <summary>
        /// 初期化メソッド
        /// </summary>
        void IInitializable.Initialize()
        {
            //ゲームシステムを起動
            _gameSystem.Initialize();
        }
    }
}
