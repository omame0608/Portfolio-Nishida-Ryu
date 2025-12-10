namespace GiikutenApplication.PackScene.Data
{
    /// <summary>
    /// レコメンドパック抽選結果DTO
    /// </summary>
    public class RecommendPackDTO
    {
        //レコメンドパック抽選結果に利用できるデータ一覧
        private readonly string _recommendUserName; //レコメンドパックで排出されるユーザ名
        private readonly string _recommendCharacterType; //レコメンドパックで排出されるジョブ名
        private readonly string _advice; //アドバイス


        public RecommendPackDTO(string recommendUserName, string recommendCharacterType, string advice)
        {
            _recommendUserName = recommendUserName;
            _recommendCharacterType = recommendCharacterType;
            _advice = advice;
        }


        //プロパティ一覧
        public string RecommendUserName => _recommendUserName;
        public string RecommendCharacterType => _recommendCharacterType;
        public string Advice => _advice;
    }
}
