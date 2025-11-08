using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Audios;
using PerfectBackParkerRev.GameCores.Repositories;
using PerfectBackParkerRev.GameCores.StageSelectSystems.Views;
using VContainer;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Sequences
{
    /// <summary>
    /// ステージセレクトシーンに入ったときのシーケンス
    /// </summary>
    public class StageSelectInSceneSequence
    {
        //システム
        [Inject] private readonly IStageSelectView _stageSelectView;
        [Inject] private readonly IStageView _stageView;
        [Inject] private readonly IStageDatabaseFacade _stageDatabaseFacade;
        [Inject] private readonly ISaveSystemFacade _saveSystemFacade;

        //Audio
        [Inject] private readonly BGMManager _bgmManager;


        /// <summary>
        /// ステージセレクトシーンに入ったときのシーケンスを再生する
        /// </summary>
        /// <param name="currentStageNumber">現在選択中のステージ番号</param>
        /// <param name="currentLatestStageNumber">現在遊べる最新ステージ</param>
        /// <returns></returns>
        public async UniTask PlaySequence(int currentStageNumber, int currentLatestStageNumber)
        {
            //ステージ情報を取得
            var stageData = _stageDatabaseFacade.GetInfoWithStageNumber(currentStageNumber);

            //スコアを取得
            var currentStageHighScore = _saveSystemFacade.LoadStageHighScore((SaveKey)currentStageNumber);
            var totalGameScore = _saveSystemFacade.GetTotalGameScore();

            //BGM再生
            _bgmManager.PlayBGM(BGM.StageSelect);

            //Viewの表示
            await _stageSelectView.ShowStageSelectView(currentStageNumber, totalGameScore, currentLatestStageNumber);
            await _stageView.ShowInSceneView(currentStageNumber, stageData, currentStageHighScore);
        }
    }
}