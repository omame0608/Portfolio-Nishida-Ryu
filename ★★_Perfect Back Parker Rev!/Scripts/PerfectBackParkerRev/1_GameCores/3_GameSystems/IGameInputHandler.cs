namespace PerfectBackParkerRev.GameCores.GameSystems
{
    /// <summary>
    /// ゲーム画面の入力ハンドラーインターフェース
    /// </summary>
    public interface IGameInputHandler
    {
        /// <summary>
        /// ユーザからの入力イベントに対する処理
        /// </summary>
        /// <param name="inputType">受け取った入力タイプ</param>
        void OnInputEvent(GameSceneInputType inputType);
    }


    /// <summary>
    /// ゲーム画面の入力タイプ
    /// </summary>
    public enum GameSceneInputType
    {
        Pause, //ポーズ
        Return, //Enterキーまたはクリック
        None
    }
}