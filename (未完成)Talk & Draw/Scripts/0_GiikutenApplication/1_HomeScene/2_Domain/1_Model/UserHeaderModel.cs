using GiikutenApplication.HomeScene.Data;
using UniRx;

namespace GiikutenApplication.HomeScene.Domain.Model
{
    /// <summary>
    /// 画面上部のユーザ情報を表示するヘッダーのデータModel
    /// </summary>
    public class UserHeaderModel
    {
        //データ本体
        public readonly ReactiveProperty<string> UserName = new(); //ユーザの名前
        public readonly ReactiveProperty<string> JobName = new(); //ユーザのジョブ名
        public readonly ReactiveProperty<int> GachaStoneAmount = new(); //ガチャ石の所持数


        /// <summary>
        /// ModelをDTOに変換したものを作成
        /// </summary>
        /// <returns>変換したDTO</returns>
        public HomeSceneDTO ConvertModelToDTO()
        {
            return new HomeSceneDTO(UserName.Value, JobName.Value, GachaStoneAmount.Value);
        } 
    }
}
