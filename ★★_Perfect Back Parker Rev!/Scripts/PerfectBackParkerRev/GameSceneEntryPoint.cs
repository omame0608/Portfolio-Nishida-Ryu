using PerfectBackParkerRev.GameCores.GameSystems;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// ゲームシーンのエントリポイント
    /// </summary>
    public class GameSceneEntryPoint : IInitializable
    {
        //システム
        [Inject] private readonly GameSystem _gameSystem;


        void IInitializable.Initialize()
        {
            _gameSystem.Initialize();
        }
    }
}