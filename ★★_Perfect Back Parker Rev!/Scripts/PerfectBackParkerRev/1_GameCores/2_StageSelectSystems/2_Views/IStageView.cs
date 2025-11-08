using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Databases;

namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Views
{
    /// <summary>
    /// ステージ選択画面の個別UIのインターフェース
    /// (現在選択しているステージ個別のUI)
    /// </summary>
    public interface IStageView
    {
        /// <summary>
        /// シーンに入ったときの表示
        /// </summary>
        /// <param name="stageNumber">遷移時に初期選択されているステージ番号</param>
        /// <param name="stageData">選択するステージのデータ</param>
        /// <param name="highScore">現在のSTAGEのハイスコア</param>
        /// <returns></returns>
        UniTask ShowInSceneView(int stageNumber, StageData stageData, int highScore);

        /// <summary>
        /// 次のステージへ移動するときの表示
        /// </summary>
        /// <param name="stageNumber">移動後のステージ番号</param>
        /// <param name="stageData">選択するステージのデータ</param>
        /// <param name="highScore">現在のSTAGEのハイスコア</param>
        /// <returns></returns>
        UniTask ShowNextStageView(int stageNumber, StageData stageData, int highScore);

        /// <summary>
        /// 前のステージへ移動するときの表示
        /// </summary>
        /// <param name="stageNumber">移動後のステージ番号</param>
        /// <param name="stageData">選択するステージのデータ</param>
        /// <param name="highScore">現在のSTAGEのハイスコア</param>
        /// <returns></returns>
        UniTask ShowPreviousStageView(int stageNumber, StageData stageData, int highScore);
    }
}