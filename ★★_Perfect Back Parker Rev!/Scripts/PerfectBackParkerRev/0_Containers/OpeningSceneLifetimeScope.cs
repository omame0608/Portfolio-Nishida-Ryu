using PerfectBackParkerRev.GameCores.OpeningSystems;
using PerfectBackParkerRev.GameCores.OpeningSystems.Sequences;
using PerfectBackParkerRev.GameCores.OpeningSystems.Views;
using PerfectBackParkerRev.OpeningHUDs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev.Containers
{
    /// <summary>
    /// オープニングシーン用DIコンテナ
    /// </summary>
    public class OpeningSceneLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("HUD")]
        [SerializeField] private OpeningView _openingView;
        [SerializeField] private GameStartTransition _gameStartTransition;


        protected override void Configure(IContainerBuilder builder)
        {
            /*-----HUDの登録-----*/
            builder.RegisterComponent(_openingView).As<IOpeningView>();
            builder.RegisterComponent(_gameStartTransition).As<IGameStartTransition>();

            /*-----ゲームシステムの登録-----*/
            //エントリポイント
            builder.RegisterEntryPoint<OpeningSceneEntryPoint>(Lifetime.Singleton);

            //オープニングシステム
            builder.Register<IOpeningInputHandler, OpeningSystem>(Lifetime.Singleton).AsSelf();

            //シーケンス
            builder.Register<OpeningSequence>(Lifetime.Singleton);
        }
    }
}

