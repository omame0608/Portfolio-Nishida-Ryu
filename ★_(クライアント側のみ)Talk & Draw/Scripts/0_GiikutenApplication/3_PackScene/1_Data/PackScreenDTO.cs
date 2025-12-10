namespace GiikutenApplication.PackScene.Data
{
    /// <summary>
    /// PackScene構築用のDTO
    /// </summary>
    public class PackScreenDTO
    {
        //PackScene構築に利用できるデータ一覧
        private readonly int _gachaStoneAmount; //ガチャ石の所持数


        public PackScreenDTO(int gachaStoneAmount)
        {
            _gachaStoneAmount = gachaStoneAmount;
        }


        //プロパティ一覧
        public int GachaStoneAmount => _gachaStoneAmount;
    }
}