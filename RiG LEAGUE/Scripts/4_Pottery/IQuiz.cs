using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クイズを表現したクラスのインターフェース
/// </summary>
/// <author>西田琉</author>
public interface IQuiz
{
    /**
     * 各種プロパティ
     * 一つの問題あたり以下の8つの情報を保持・管理してください。
     */
    public int QuizID { get; } //クイズのID
    public int TimeLimit { get; } //制限時間
    public string QuizSentence { get; } //問題文
    public string Select1 { get; } //選択肢1
    public string Select2 { get; } //選択肢2
    public string Select3 { get; } //選択肢3
    public int CorrectNumber { get; } //正解の選択肢番号
    public string AnswerSentence { get; } //解説文


    /// <summary>
    /// クイズの正誤判定を行う
    /// </summary>
    /// <param name="selectedNumber">解答された選択肢番号</param>
    /// <returns>正誤をtrue/falseで返す</returns>
    public bool JudgeAnswer(int selectedNumber);
    /**
     * ↑引数に1,2,3以外の数字が渡されたらエラーを吐くようにしてほしいです。
     */
}
