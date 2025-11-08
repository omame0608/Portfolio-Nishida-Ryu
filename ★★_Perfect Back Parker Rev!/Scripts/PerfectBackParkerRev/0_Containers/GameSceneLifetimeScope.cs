using PerfectBackParkerRev.GameCores.GameSystems;
using PerfectBackParkerRev.GameCores.GameSystems.EndConditions;
using PerfectBackParkerRev.GameCores.GameSystems.ItemPickUps;
using PerfectBackParkerRev.GameCores.GameSystems.Scores;
using PerfectBackParkerRev.GameCores.GameSystems.Sequences;
using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.Users;
using PerfectBackParkerRev.GameHUDs;
using PerfectBackParkerRev.Players;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev.Containers
{
    /// <summary>
    /// ゲームシーン用のDIコンテナ
    /// </summary>
    public class GameSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("Player")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerCameraController _cameraController;

        [Header("終了判定のための監視対象")]
        [SerializeField] private TestSpaceDetector _testSpaceDetector; //テスト用
        [SerializeField] private TestShiftDetector _testShiftDetector; //テスト用

        [Header("HUD")]
        [SerializeField] private WaveStartView _waveStartView;
        [SerializeField] private WaveTimerView _waveTimerView;
        [SerializeField] private ClearCountView _clearCountView;
        [SerializeField] private FailedView _failedView;
        [SerializeField] private ClearView _clearView;
        [SerializeField] private StageStartView _stageStartView;
        [SerializeField] private StageFinishView _stageFinishView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private StageFinishTransition _stageFinishTransition;

        //test制御用
        [Header("テスト用：適用するルールを選択")]
        [SerializeField] private bool _testSpaceClearRule;
        [SerializeField] private bool _testShiftFailedRule;
        [SerializeField] private bool _obstacleCollisionRule = true;
        [SerializeField] private bool _goalRule = true;
        [SerializeField] private bool _timeUpRule = true;


        protected override void Configure(IContainerBuilder builder)
        {
            /*-----Playerの登録-----*/
            builder.RegisterComponent(_playerController).As<IPlayerSystem>();
            builder.RegisterComponent(_cameraController).As<IPlayerCameraSystem>();

            /*-----HUDの登録-----*/
            builder.RegisterComponent(_waveStartView).As<IWaveStartView>();
            builder.RegisterComponent(_waveTimerView).As<IWaveTimerView>();
            builder.RegisterComponent(_clearCountView).As<IClearCountView>();
            builder.RegisterComponent(_failedView).As<IFailedView>();
            builder.RegisterComponent(_clearView).As<IClearView>();
            builder.RegisterComponent(_stageStartView).As<IStageStartView>();
            builder.RegisterComponent(_stageFinishView).As<IStageFinishView>();
            builder.RegisterComponent(_pauseView).As<IPauseView>();
            builder.RegisterComponent(_stageFinishTransition).As<IStageFinishTransition>();

            /*-----ゲームシステムの登録-----*/
            //エントリポイント
            builder.RegisterEntryPoint<GameSceneEntryPoint>(Lifetime.Singleton);

            //ゲームシステム
            builder.Register<IGameInputHandler, GameSystem>(Lifetime.Singleton).AsSelf();

            //シーケンス
            builder.Register<StageStartSequence>(Lifetime.Singleton);
            builder.Register<WaveSequence>(Lifetime.Singleton);
            builder.Register<StageFinishSequence>(Lifetime.Singleton);

            //制御用パーツ
            builder.Register<WaveTimer>(Lifetime.Singleton);

            //ゲーム(ウェーブ)終了条件
            builder.Register<WaveFinishableCollection>(Lifetime.Singleton);
            //if (_testSpaceClearRule) builder.Register<IWaveFinishable, TestClearRule>(Lifetime.Singleton); //テスト用
            //if (_testShiftFailedRule) builder.Register<IWaveFinishable, TestFailedRule>(Lifetime.Singleton); //テスト用
            if (_obstacleCollisionRule) builder.Register<IWaveFinishable, ObstacleCollisionRule>(Lifetime.Singleton).Keyed("ObstacleCollisionRule");
            if (_goalRule) builder.Register<IWaveFinishable, GoalRule>(Lifetime.Singleton).Keyed("GoalRule");
            if (_timeUpRule) builder.Register<IWaveFinishable, TimeUpRule>(Lifetime.Singleton).Keyed("TimeUpRule");

            //ゲーム(ウェーブ)終了判定に必要なMonoレイヤーの監視対象
            //動的な登録が必要なものは個別にresolver等を利用して登録
            //監視対象がロジックレイヤーの場合はクラスで上記のうちから直接解決
            //builder.RegisterInstance(_testSpaceDetector).As<IWaveFinishNotifier>().Keyed("TestClearRule"); //テスト用
            //builder.RegisterInstance(_testShiftDetector).As<IWaveFinishNotifier>().Keyed("TestFailedRule"); //テスト用

            //アイテム
            builder.Register<IGoldenScrewGetNotifierRegistrar, GoldenScrewStatus>(Lifetime.Singleton).AsSelf();

            //スコア
            builder.Register<ScoreStatus>(Lifetime.Singleton);
        }
    }
}