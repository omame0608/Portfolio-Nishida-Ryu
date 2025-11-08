using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.Databases;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.Savedatas;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev.Containers
{
    /// <summary>
    /// プロジェクト共有DIコンテナ
    /// </summary>
    public class RootLifetimeScope : LifetimeScope
    {
        //MonoBehaviour継承クラスの参照
        [Header("DB")]
        [SerializeField] private StageDataAsset _stageDataAsset; //ステージデータアセット
        [Header("Audio")]
        [SerializeField] private SEManager _seManager; //SEマネージャー
        [SerializeField] private BGMManager _bgmManager; //BGMマネージャー


        protected override void Configure(IContainerBuilder builder)
        {
            //アセット
            builder.RegisterComponent(_stageDataAsset); //ステージデータアセット

            //グローバルサービス
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf(); //シーンローダー
            builder.Register<IStageDatabaseFacade, StageDatabaseFacade>(Lifetime.Singleton); //ステージDB窓口
            builder.Register<SaveSystemFacade>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf(); //セーブシステム窓口
            builder.RegisterComponentInNewPrefab(_seManager, Lifetime.Singleton); //SEマネージャー
            builder.RegisterComponentInNewPrefab(_bgmManager, Lifetime.Singleton); //BGMマネージャー
        }
    }
}