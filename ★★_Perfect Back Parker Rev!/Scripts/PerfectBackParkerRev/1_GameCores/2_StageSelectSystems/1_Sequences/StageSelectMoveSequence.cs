using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using VContainer;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Sequences
{
    /// <summary>
    /// ステージセレクトでステージを移動するときのシーケンス
    /// </summary>
    public class StageSelectMoveSequence
    {
        //システム
        [Inject] private readonly IStageView _stageView;
        [Inject] private readonly IStageDatabaseFacade _stageDatabaseFacade;
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;


        /// <summary>
        /// 選択中のステージが変更されたときのシーケンスを再生する
        /// </summary>
        /// <param name="isMoveNextStage">trueなら次のステージへ移動、falseなら前のステージへ移動</param>
        /// <param name="stageNumber">移動後のステージ番号</param>
        /// <returns></returns>
        public async UniTask PlaySequence(bool isMoveNextStage, int stageNumber)
        {
            //ステージ情報を取得
            var stageData = _stageDatabaseFacade.GetInfoWithStageNumber(stageNumber);

            //現在のステージのハイスコアを取得
            var currentStageHighScore = _saveSystemFacade.LoadStageHighScore((SaveKey)stageNumber);

            //次のステージへ移動
            if (isMoveNextStage)
            {
                await _stageView.ShowNextStageView(stageNumber, stageData, currentStageHighScore);
            }
            //前のステージへ移動
            else
            {
                await _stageView.ShowPreviousStageView(stageNumber, stageData, currentStageHighScore);
            }
        }
    }
}