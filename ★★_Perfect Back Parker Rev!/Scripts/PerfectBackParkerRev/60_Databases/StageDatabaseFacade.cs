using PerfectBackParkerRev.GameCores.Repositories;
using VContainer;

namespace PerfectBackParkerRev.Databases
{
    /// <summary>
    /// ステージDB窓口
    /// </summary>
    public class StageDatabaseFacade : IStageDatabaseFacade
    {
        //DB
        [Inject] private readonly StageDataAsset _database;


        public StageData GetInfoWithStageNumber(int number)
        {
            //番号からインデックスに変換
            int index = number - 1;

            return _database.StageDataList[index];
        }
    }
}
