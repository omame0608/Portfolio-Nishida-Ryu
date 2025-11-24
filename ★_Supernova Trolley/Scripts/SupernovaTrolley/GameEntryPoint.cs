using SupernovaTrolley.GameCores;
using VContainer;
using VContainer.Unity;

namespace SupernovaTrolley
{
    /// <summary>
    /// ゲームのエントリポイント
    /// </summary>
    public class GameEntryPoint : IInitializable
    {
        //システム
        [Inject] private readonly GameSystem _gameSystem;


        void IInitializable.Initialize()
        {
            //ゲームシステムを起動する
            _gameSystem.Initialize();
        }
    }
}