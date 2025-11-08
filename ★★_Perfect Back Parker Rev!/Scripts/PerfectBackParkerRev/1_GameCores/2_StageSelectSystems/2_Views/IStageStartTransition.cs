namespace PerfectBackParkerRev.GameCores.StageSelectSystems.Views
{
    /// <summary>
    /// ステージ開始のトランジションインターフェース
    /// </summary>
    public interface IStageStartTransition
    {
        /// <summary>
        /// 各ゲーム画面へ遷移するトランジションを再生する
        /// </summary>
        /// <param name="nextSceneType">遷移するSTAGEシーン</param>
        void AwakeTransition(SceneType nextSceneType);
    }
}
