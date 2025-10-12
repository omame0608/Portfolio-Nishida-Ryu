using Cysharp.Threading.Tasks;
using ParkingGame.GameSystems.EndCondition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ParkingGame.Field
{
    /// <summary>
    /// ステージプレハブの初期化処理を行うクラス
    /// </summary>
    public class StageInitialization : MonoBehaviour
    {
        //参照
        [Inject] private FieldCollisionRule _fieldCollisionRule;
        [Inject] private GoalRule _goalRule;
        [Inject] private IObjectResolver _resolver;

        //障害物リスト
        private CollisionDetector[] _colliders;

        //ゴール
        private GoalDetector _goal;


        void Start()
        {
            //ステージプレハブに含まれるコライダーを全て取得
            _colliders = GetComponentsInChildren<CollisionDetector>();

            //障害物としてルールに登録
            foreach (var c in _colliders)
            {
                _fieldCollisionRule.Obstacles.Add(c);
            }

            //ゴールを取得
            _goal = GetComponentInChildren<GoalDetector>();
            _resolver.Inject(_goal);

            //ゴールとしてルールに登録
            _goalRule.GoalDetector = _goal;
        }


        void OnDestroy()
        {
            //障害物をルールから削除
            foreach (var c in _colliders)
            {
                _fieldCollisionRule.Obstacles.Remove(c);
            }
        }
    }
}