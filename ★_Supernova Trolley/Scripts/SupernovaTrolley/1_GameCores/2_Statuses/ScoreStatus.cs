using System;
using Utilities;

namespace SupernovaTrolley.GameCores.Statuses
{
    /// <summary>
    /// スコアを管理するステータスクラス
    /// </summary>
    public class ScoreStatus
    {
        //ステータス
        private int _currentScore; //現在のスコア
        public int CurrentScore => _currentScore; //現在のスコア取得用プロパティ


        /// <summary>
        /// スコアステータスの初期化
        /// </summary>
        public void InitStatus()
        {
            _currentScore = 0;
        }


        /// <summary>
        /// スコアを加点する
        /// </summary>
        /// <param name="add">加点する値</param>
        public void AddScore(int add)
        {
            if (add > 0)
            {
                _currentScore += add;
                MyLogger.Log($"加点!!現在のスコア：{_currentScore}");
            }
            else
            {
                MyLogger.LogError($"負の値がスコアに加算されています");
            }
        }


        /// <summary>
        /// スコアを減点する
        /// </summary>
        /// <param name="sub">減点する値</param>
        public void SubtractScore(int sub)
        {
            if (sub > 0)
            {
                _currentScore = Math.Max(0, _currentScore - sub);
                MyLogger.Log($"減点!!現在のスコア：{_currentScore}");
            }
            else
            {
                MyLogger.LogError($"負の値がスコアから減算されています");
            }
        }
    }
}