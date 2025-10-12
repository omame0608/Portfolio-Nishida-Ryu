using ParkingGame.Audio;
using ParkingGame.Data;
using ParkingGame.Field;
using ParkingGame.GameSystems;
using ParkingGame.GameSystems.Database;
using ParkingGame.GameSystems.EndCondition;
using ParkingGame.GameSystems.Sequences;
using ParkingGame.GameSystems.View;
using ParkingGame.HUD;
using ParkingGame.Player;
using System;
using System.Collections.Generic;
using Test;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ParkingGame.Container
{
    /// <summary>
    /// ゲーム全体のDIコンテナ
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("DB")]
        [SerializeField] private StageDataAsset _stageDataAsset;

        [Header("HUD")]
        [SerializeField] private OpeningView _openingView;
        [SerializeField] private StageStartView _stageStartView;
        [SerializeField] private StageTimerView _stageTimerView;
        [SerializeField] private ClearCountView _clearCountView;
        [SerializeField] private WinResultView _winResultView;
        [SerializeField] private LoseResultView _loseResultView;
        [SerializeField] private EndingView _endingView;

        [Header("Player")]
        [SerializeField] private CarController _carController;
        [SerializeField] private CameraController _cameraController;

        [Header("当たり判定")]
        [SerializeField] private CollisionDetector _fieldCollider;

        [Header("Audio")]
        [SerializeField] private SEManager _seManager;
        [SerializeField] private BGMManager _bgmManager;

        //テスト
        [Header("テスト")]
        [SerializeField] private CheckSpace _checkSpace;
        [SerializeField] private CheckShift _checkShift;


        protected override void Configure(IContainerBuilder builder)
        {
            //GameSystemの登録
            builder.RegisterEntryPoint<ParkingGameEntryPoint>();
            builder.Register<GameSystem>(Lifetime.Singleton);
            builder.Register<StageSequence>(Lifetime.Singleton);
            builder.Register<OpeningSequence>(Lifetime.Singleton);
            builder.Register<EndingSequence>(Lifetime.Singleton);
            builder.Register<ParkingGameFinishableCollection>(Lifetime.Singleton);
            builder.Register<IStageDatabaseFacade, StageDatabaseFacade>(Lifetime.Singleton);
            builder.Register<StageTimer>(Lifetime.Singleton);

            //DBの登録
            builder.RegisterComponent(_stageDataAsset);

            //HUDの登録
            builder.RegisterComponent(_openingView).As<IOpeningView>();
            builder.RegisterComponent(_stageStartView).As<IStageStartView>();
            builder.RegisterComponent(_stageTimerView).As<IStageTimerView>();
            builder.RegisterComponent(_clearCountView).As<IClearCountView>();
            builder.RegisterComponent(_winResultView).As<IWinResultView>();
            builder.RegisterComponent(_loseResultView).As<ILoseResultView>();
            builder.RegisterComponent(_endingView).As<IEndingView>();

            //Playerの登録
            builder.RegisterComponent(_carController).As<IPlayerSystem>();
            builder.RegisterComponent(_cameraController).As<ICameraSystem>();

            //ルールの登録
            builder.Register<IParkingGameFinishable, FieldCollisionRule>(Lifetime.Singleton).AsSelf();
            builder.Register<IParkingGameFinishable, GoalRule>(Lifetime.Singleton).AsSelf();
            builder.Register<IParkingGameFinishable, TimeUpRule>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponent(_fieldCollider);

            //Audioの登録
            builder.RegisterComponent(_seManager);
            builder.RegisterComponent(_bgmManager);

            //テスト
            //builder.Register<IParkingGameFinishable, MocSpaceClickRule>(Lifetime.Singleton);
            //builder.Register<IParkingGameFinishable, MocShiftClickRule>(Lifetime.Singleton);
            //builder.RegisterComponent(_checkShift);
            //builder.RegisterComponent(_checkSpace);
        }
    }
}
