using Cysharp.Threading.Tasks;
using GiikutenApplication.HomeScene.Data;

namespace GiikutenApplication.HomeScene.Domain
{
    /// <summary>
    /// HomeSceneとサーバを繋ぐ窓口インターフェース
    /// </summary>
    public interface IHomeSceneRepository
    {
        /// <summary>
        /// サーバからHomeScene構築用のDTOを読み込む
        /// </summary>
        /// <returns>サーバから読み込んだHomeScene構築用のDTO</returns>
        UniTask<HomeSceneDTO> Fetch();


        /// <summary>
        /// サーバへHomeScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">サーバへ保存するHomeScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        UniTask<bool> Save(HomeSceneDTO dto);
    }
}
