using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.Notifiers;
using UnityEngine;
using VContainer;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// ウェーブプレハブ生成時の初期化処理を行う
    /// </summary>
    public class WavePrefabInitialization : MonoBehaviour
    {
        //システム
        [Inject, Key("ObstacleCollisionRule")] private readonly IWaveFinishable _obstacleCollisionRule;
        [Inject, Key("GoalRule")] private readonly IWaveFinishable _goalRule;
        [Inject] private readonly IGoldenScrewGetNotifierRegistrar _goldenScrewRegistrar;
        [Inject] private readonly IObjectResolver _resolver;


        /// <summary>
        /// ウェーブプレハブ生成時の初期化を行う
        /// 主にイベント検出対象となる障害物やゴール枠をルールロジックへ登録する
        /// </summary>
        public void Init()
        {
            //動的生成した障害物をルールへ登録
            var obstaclesArray = GetComponentsInChildren<ObstacleDetector>();
            foreach (var obstacle in obstaclesArray)
            {
                _obstacleCollisionRule.AddNotifier(obstacle);
            }

            //元からあった障害物もルールへ再登録
            var fieldBase = GameObject.FindWithTag("FieldBase").GetComponent<ObstacleDetector>();
            _obstacleCollisionRule.AddNotifier(fieldBase);

            //ゴール枠をルールへ登録
            var goal = GetComponentInChildren<GoalDetector>();
            _resolver.Inject(goal);
            _goalRule.AddNotifier(goal);

            //「金のネジをルールへ登録」
            var screw = GetComponentInChildren<GoldenScrewDetector>();
            _goldenScrewRegistrar.AddNotifier(screw);
        }
    }
}