using PerfectBackParkerRev.GameCores.StageSelectSystems;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// ステージセレクトシーンのエントリポイント
    /// </summary>
    public class StageSelectSceneEntryPoint : IInitializable
    {
        //システム
        [Inject] private readonly StageSelectSystem _stageSelectSystem;


        void IInitializable.Initialize()
        {
            _stageSelectSystem.Initialize();
        }
    }
}