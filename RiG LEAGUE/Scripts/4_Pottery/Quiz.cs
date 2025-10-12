using UnityEngine;
using System.Linq;

public class Quiz : IQuiz
{
    public int QuizID { get; private set; }

    public int TimeLimit { get; private set; }

    public string QuizSentence { get; private set; }

    public string Select1 { get; private set; }

    public string Select2 { get; private set; }

    public string Select3 { get; private set; }

    public int CorrectNumber { get; private set; }

    public string AnswerSentence { get; private set; }



    private int[] ValidNumbers = { 1, 2, 3 }; // 有効な問題番号

    /// <summary>
    /// 回答した答えの正誤を判別
    /// </summary>
    /// <param name="selectedNumber">選んだ回答番号</param>
    /// <returns>正:true 誤:false</returns>
    public bool JudgeAnswer(int selectedNumber)
    {

        if (!ValidNumbers.Contains(selectedNumber))　//選ばれた番号の正当性を確認
        {
            Debug.LogError("選択された番号は有効ではありません");
        }

        if (selectedNumber == CorrectNumber) return true;　//合っていればtrueを返す
        else return false;
    }

    /// <summary>
    /// データベースから該当のクイズデータをセット
    /// </summary>
    /// <param name="quizData">指定されたidに基づくクイズのデータ</param>
    public void SetQuizData(QuizProperty quizData)
    {
        QuizID = quizData.QuizID;

        TimeLimit = quizData.TimeLimit;

        QuizSentence = quizData.QuizSentence;

        Select1 = quizData.Select1;

        Select2 = quizData.Select2;

        Select3 = quizData.Select3;

        CorrectNumber = quizData.CorrectNumber;

        AnswerSentence = quizData.AnswerSentence;
    }
}
