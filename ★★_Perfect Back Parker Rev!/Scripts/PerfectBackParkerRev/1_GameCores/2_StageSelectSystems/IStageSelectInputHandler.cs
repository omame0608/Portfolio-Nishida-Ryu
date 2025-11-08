namespace PerfectBackParkerRev.GameCores.StageSelectSystems
{
    /// <summary>
    /// ステージセレクト画面の入力ハンドラーインターフェース
    /// </summary>
    public interface IStageSelectInputHandler
    {
        /// <summary>
        /// ユーザからの入力イベントに対する処理
        /// </summary>
        /// <param name="inputType">受け取った入力タイプ</param>
        void OnInputEvent(StageSelectSceneInputType inputType);
    }


    /// <summary>
    /// ステージセレクト画面の入力タイプ
    /// </summary>
    public enum StageSelectSceneInputType
    {
        MoveNextStage, //次のステージを選択
        MovePreviousStage, //前のステージを選択
        Settings, //設定
        StartStage, //ステージ開始
        BackGame, //ゲームに戻る
        None
    }
}