namespace PerfectBackParkerRev.GameCores.OpeningSystems
{
    /// <summary>
    /// オープニング画面の入力ハンドラーインターフェース
    /// </summary>
    public interface IOpeningInputHandler
    {
        /// <summary>
        /// ユーザからの入力イベントに対する処理
        /// </summary>
        /// <param name="inputType">受け取った入力タイプ</param>
        void OnInputEvent(OpeningSceneInputType inputType);
    }


    /// <summary>
    /// オープニング画面の入力タイプ
    /// </summary>
    public enum OpeningSceneInputType
    {
        StartGame, //ゲーム開始
        None
    }
}