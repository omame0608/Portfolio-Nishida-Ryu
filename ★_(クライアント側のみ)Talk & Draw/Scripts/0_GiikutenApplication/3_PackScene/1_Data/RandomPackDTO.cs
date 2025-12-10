namespace GiikutenApplication.PackScene.Data
{
    /// <summary>
    /// ランダムパック抽選結果DTO
    /// </summary>
    public class RandomPackDTO
    {
        //ランダムパック抽選結果に利用できるデータ一覧
        private readonly string _randomUserName; //ランダムパックで排出されるユーザ名
        private readonly string _randomCharacterType; //ランダムパックで排出されるジョブ名


        public RandomPackDTO(string randomUserName, string randomCharacterType)
        {
            _randomUserName = randomUserName;
            _randomCharacterType = randomCharacterType;
        }


        //プロパティ一覧
        public string RandomUserName => _randomUserName;
        public string RandomCharacterType => _randomCharacterType;
    }
}
