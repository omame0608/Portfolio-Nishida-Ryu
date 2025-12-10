using Cysharp.Threading.Tasks;
using GiikutenApplication.PackScene.Data;

namespace GiikutenApplication.PackScene.Domain
{
    /// <summary>
    /// PackSceneとサーバを繋ぐ窓口インターフェース
    /// </summary>
    public interface IPackSceneRepository
    {
        /// <summary>
        /// サーバからPackScene構築用のDTOを読み込む
        /// </summary>
        /// <returns>サーバから読み込んだPackScene構築用のDTO</returns>
        UniTask<PackScreenDTO> Fetch();


        /// <summary>
        /// サーバへPackScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">サーバへ保存するPackScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        UniTask<bool> Save(PackScreenDTO dto);


        /// <summary>
        /// サーバからランダムパック抽選結果のDTOを読み込む
        /// </summary>
        /// <returns>サーバから読み込んだランダムパック抽選結果のDTO</returns>
        UniTask<RandomPackDTO> FetchRandomPack();


        /// <summary>
        /// サーバからレコメンドパック抽選結果のDTOを読み込む
        /// </summary>
        /// <returns>サーバから読み込んだレコメンドパック抽選結果のDTO</returns>
        UniTask<RecommendPackDTO> FetchRecommendPack();
    }
}
