using PerfectBackParkerRev.GameCores.OpeningSystems;
using VContainer;
using VContainer.Unity;

namespace PerfectBackParkerRev
{
    /// <summary>
    /// オープニングシーンのエントリポイント
    /// </summary>
    public class OpeningSceneEntryPoint : IInitializable
    {
        //システム
        [Inject] private readonly OpeningSystem _openingSystem;


        void IInitializable.Initialize()
        {
            _openingSystem.Initialize();
        }
    }
}