using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クイズのスタブ
/// </summary>
/// <author>西田琉</author>
public class StubQuiz : IQuiz
{
    public int QuizID => 99;

    public int TimeLimit => 8;

    public string QuizSentence => "これはテスト問題です。正解は1です。";

    public string Select1 => "選択肢1";

    public string Select2 => "選択肢2";

    public string Select3 => "選択肢3";

    public int CorrectNumber => 1;

    public string AnswerSentence => "これは問題の解説文章です";

    public bool JudgeAnswer(int selectedNumber)
    {
        return selectedNumber == CorrectNumber;
    }
}
