using GiikutenApplication.PackScene.Data;
using UniRx;

namespace GiikutenApplication.PackScene.Domain.Model
{
    /// <summary>
    /// Pack画面で利用するデータModel
    /// </summary>
    public class PackScreenModel
    {
        //データ本体
        public readonly ReactiveProperty<int> GachaStoneAmount = new(); //ガチャ石の所持数


        /// <summary>
        /// ModelをDTOに変換したものを作成
        /// </summary>
        /// <returns>変換したDTO</returns>
        public PackScreenDTO ConvertModelToDTO()
        {
            return new PackScreenDTO(GachaStoneAmount.Value);
        }
    }
}