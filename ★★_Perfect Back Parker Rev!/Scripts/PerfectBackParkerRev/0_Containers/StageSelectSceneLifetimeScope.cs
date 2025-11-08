using PerfectBackParkerRev.GameCores.GameSystems.Views;
using PerfectBackParkerRev.GameCores.StageSelectSystems;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Sequences;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using PerfectBackParkerRev.GameHUDs;
using PerfectBackParkerRev.StageSelectHUDs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev.Containers
{
    /// <summary>
    /// ステージセレクトシーン用のDIコンテナ
    /// </summary>
    public class StageSelectSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("HUD")]
        [SerializeField] private StageSelectView _stageSelectView;
        [SerializeField] private StageView _stageView;
        [SerializeField] private EndingView _endingView;
        [SerializeField] private ClearCountView _clearCountView;
        [SerializeField] private StageStartTransition _stageStartTransition;


        protected override void Configure(IContainerBuilder builder)
        {
            /*-----HUDの登録-----*/
            builder.RegisterComponent(_stageSelectView).As<IStageSelectView>();
            builder.RegisterComponent(_stageView).As<IStageView>();
            builder.RegisterComponent(_endingView).As<IEndingView>();
            builder.RegisterComponent(_clearCountView).As<IClearCountView>();
            builder.RegisterComponent(_stageStartTransition).As<IStageStartTransition>();


            /*-----ゲームシステムの登録-----*/
            //エントリポイント
            builder.RegisterEntryPoint<StageSelectSceneEntryPoint>(Lifetime.Singleton);

            //ステージセレクトシステム
            builder.Register<IStageSelectInputHandler, StageSelectSystem>(Lifetime.Singleton).AsSelf();

            //シーケンス
            builder.Register<StageSelectInSceneSequence>(Lifetime.Singleton);
            builder.Register<StageSelectMoveSequence>(Lifetime.Singleton);
            builder.Register<EndingSequence>(Lifetime.Singleton);
        }
    }
}
