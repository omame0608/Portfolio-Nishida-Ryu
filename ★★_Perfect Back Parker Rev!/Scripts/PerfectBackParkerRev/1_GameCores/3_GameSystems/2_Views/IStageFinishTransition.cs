namespace PerfectBackParkerRev.GameCores.GameSystems.Views
{
    /// <summary>
    /// ステージ終了のトランジション
    /// </summary>
    public interface IStageFinishTransition
    {
        /// <summary>
        /// ステージ選択画面へ遷移するトランジションを再生する
        /// </summary>
        /// <param name="currentSceneType">現在のSTAGEシーン</param>
        /// <param name="isReplay">同じステージのリプレイか</param>
        void AwakeTransition(SceneType currentSceneType, bool isReplay = false);
    }
}