using SupernovaTrolley.Audios;
using SupernovaTrolley.Directions;
using SupernovaTrolley.Enemies;
using SupernovaTrolley.GameCores;
using SupernovaTrolley.GameCores.EndConditions;
using SupernovaTrolley.GameCores.Sequences;
using SupernovaTrolley.GameCores.Statuses;
using SupernovaTrolley.HUDs;
using SupernovaTrolley.Players;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SupernovaTrolley.Container
{
    /// <summary>
    /// DIコンテナ
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        //Mono継承の参照
        [Header("Player")]
        [SerializeField] private Player _player;

        [Header("HUD")]
        [SerializeField] private EndingView _endingView;
        [SerializeField] private StatusView _statusView;

        [Header("Monoシステム")]
        [SerializeField] private RailScrollSystem _railScrollSystem;
        [SerializeField] private SkyboxSystem _skyboxSystem;
        [SerializeField] private EnemySystem _enemySystem;

        [Header("Facade")]
        [SerializeField] private GameSystemFacade _gameSystemFacade;

        [Header("Sounds")]
        [SerializeField] private BGMManager _bgmManager;
        [SerializeField] private SEManager _seManager;


        protected override void Configure(IContainerBuilder builder)
        {
            //----------Player----------
            builder.RegisterComponent(_player);

            //----------HUD----------
            builder.RegisterComponent(_endingView);
            builder.RegisterComponent(_statusView);


            //----------Monoシステム----------
            builder.RegisterComponent(_railScrollSystem);
            builder.RegisterComponent(_skyboxSystem);
            builder.RegisterComponent(_enemySystem);

            //----------Facade----------
            builder.RegisterComponent(_gameSystemFacade);

            //----------Sounds----------
            builder.RegisterComponent(_bgmManager);
            builder.RegisterComponent(_seManager);


            //----------ゲームシステム----------
            //エントリポイント
            builder.RegisterEntryPoint<GameEntryPoint>(Lifetime.Singleton);

            //ゲームシステム
            builder.Register<GameSystem>(Lifetime.Singleton);

            //シーケンス
            builder.Register<OpeningSequence>(Lifetime.Singleton);
            builder.Register<GameSequence>(Lifetime.Singleton);
            builder.Register<EndingSequence>(Lifetime.Singleton);

            //ステータス
            //builder.Register<HitPointStatus>(Lifetime.Singleton);
            builder.Register<ScoreStatus>(Lifetime.Singleton);
            builder.Register<TimeStatus>(Lifetime.Singleton);

            //ゲーム終了管理
            builder.Register<GameFinishableCollection>(Lifetime.Singleton);
            //builder.Register<IGameFinishable, HitPointRule>(Lifetime.Singleton);
            builder.Register<IGameFinishable, GoalRule>(Lifetime.Singleton);
        }
    }
}