using Cysharp.Threading.Tasks;
using PerfectBackParkerRev.Databases;

namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ステージ開始時のビューインターフェース
    /// </summary>
    public interface IStageStartView
    {
        /// <summary>
        /// ステージスタートの紹介ビューを表示する
        /// </summary>
        /// <param name="stageData">進行中のSTAGEデータ</param>
        /// <param name="highScore">現在のSTAGEのハイスコア</param>
        /// <returns></returns>
        UniTask ShowStageAbstractView(StageData stageData, int highScore);

        /// <summary>
        /// ステージスタートの紹介ビューの再生をキャンセルし、再生終了後の状態へ即座に遷移する
        /// </summary>
        void CancelShowStageAbstractView();

        /// <summary>
        /// STAGE開始テキストを表示する
        /// </summary>
        /// <returns></returns>
        UniTask ShowStageStartText();
    }
}