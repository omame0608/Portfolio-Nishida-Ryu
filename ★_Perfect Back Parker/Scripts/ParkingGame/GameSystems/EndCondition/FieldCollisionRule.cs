using ParkingGame.Field;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;

namespace ParkingGame.GameSystems.EndCondition
{
    /// <summary>
    /// フィールドのコライダーに当たったら失敗
    /// </summary>
    public class FieldCollisionRule : IParkingGameFinishable
    {
        //監視対象の静的コライダー（全ステージ共通の壁等）
        [Inject] private readonly CollisionDetector _walls;

        //監視対象の動的コライダー（各ステージの障害物等）
        //動的にコライダーを生成する場合はここに登録する
        public List<CollisionDetector> Obstacles = new List<CollisionDetector>();


        public void StartObserve(Action<GameResultType> finishedCallback)
        {
            _walls.OnCollision.Subscribe(_ => finishedCallback(GameResultType.Lose));
            Obstacles.ForEach(collider => collider.OnCollision.Subscribe(_ => finishedCallback(GameResultType.Lose))); 
        }
    }
}