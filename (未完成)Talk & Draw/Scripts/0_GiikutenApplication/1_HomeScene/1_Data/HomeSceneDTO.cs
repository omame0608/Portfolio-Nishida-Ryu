namespace GiikutenApplication.HomeScene.Data
{
    /// <summary>
    /// HomeScene構築用のDTO
    /// </summary>
    public class HomeSceneDTO
    {
        //HomeScene構築に利用できるデータ一覧
        private readonly string _userName; //ユーザの名前
        private readonly string _jobName; //ユーザのジョブ名
        private readonly int _gachaStoneAmount; //ガチャ石の所持数


        public HomeSceneDTO(string userName, string jobName, int gachaStoneAmount)
        {
            _userName = userName;
            _jobName = jobName;
            _gachaStoneAmount = gachaStoneAmount;
        }


        //プロパティ一覧
        public string UserName => _userName;
        public string JobName => _jobName;
        public int GachaStoneAmount => _gachaStoneAmount;
    }
}
