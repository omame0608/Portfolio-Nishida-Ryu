using System;

namespace SupernovaTrolley.GameCores.EndConditions
{
    /// <summary>
    /// ゲーム終了条件を判定するインターフェース
    /// </summary>
    public interface IGameFinishable
    {
        /// <summary>
        /// ゲーム終了条件の監視を開始する
        /// </summary>
        /// <param name="gameFinishCallback">終了判定のコールバック</param>
        void StartObserve(Action<GameResultType> gameFinishCallback);

        /// <summary>
        /// ゲーム終了条件の監視を停止する
        /// </summary>
        void StopObserve();
    }


    /// <summary>
    /// ゲーム終了の種類
    /// </summary>
    public enum GameResultType
    {
        None = 0,
        Clear = 1,
        GameOver = 2
    }
}