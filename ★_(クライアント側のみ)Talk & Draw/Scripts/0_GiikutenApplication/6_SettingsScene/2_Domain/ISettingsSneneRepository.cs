using Cysharp.Threading.Tasks;
using GiikutenApplication.SettingsScene.Data;

namespace GiikutenApplication.SettingsScene.Domain
{
    /// <summary>
    /// SettingsSceneとサーバを繋ぐ窓口インターフェース
    /// </summary>
    public interface ISettingsSneneRepository
    {
        /// <summary>
        /// サーバからSettingsScene構築用のDTOを読み込む
        /// </summary>
        /// <returns>サーバから読み込んだSettingsScene構築用のDTO</returns>
        UniTask<SettingsInDTO> Fetch();


        /// <summary>
        /// サーバへSettingsScene構築用のDTOを書き込む
        /// </summary>
        /// <param name="dto">サーバへ保存するSettingsScene構築用のDTO</param>
        /// <returns>書き込みに成功したかどうか</returns>
        UniTask<bool> Save(SettingsInDTO dto);
    }
}
