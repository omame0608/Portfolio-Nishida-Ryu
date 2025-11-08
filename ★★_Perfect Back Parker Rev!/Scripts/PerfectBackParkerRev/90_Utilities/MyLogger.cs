using UnityEngine;

namespace PerfectBackParkerRev.Utilities
{
    /// <summary>
    /// ログ出力ユーティリティ
    /// 開発時のみログを出力する
    /// </summary>
    public static class MyLogger
    {
        /// <summary>
        /// 通常ログ出力
        /// </summary>
        public static void Log(string message, bool showInConsole = true)
        {
#if UNITY_EDITOR
            if (showInConsole) Debug.Log(message);
#endif
        }


        /// <summary>
        /// 警告ログ出力
        /// </summary>
        public static void LogWarning(string message, bool showInConsole = true)
        {
#if UNITY_EDITOR
            if (showInConsole) Debug.LogWarning(message);
#endif
        }

        
        /// <summary>
        /// エラーログ出力
        /// </summary>
        public static void LogError(string message, bool showInConsole = true)
        {
#if UNITY_EDITOR
            if (showInConsole) Debug.LogError(message);
#endif
        }


        /// <summary>
        /// 例外ログ出力
        /// </summary>
        public static void LogException(System.Exception ex, bool showInConsole = true)
        {
#if UNITY_EDITOR
            if (showInConsole) Debug.LogException(ex);
#endif
        }
    }
}