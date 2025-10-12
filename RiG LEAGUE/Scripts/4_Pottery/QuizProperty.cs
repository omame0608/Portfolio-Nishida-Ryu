using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizData", menuName = "Quiz/QuizData")]
public class QuizProperty : ScriptableObject
{
    public int QuizID;

    public int TimeLimit;

    public string QuizSentence;

    public string Select1;

    public string Select2;

    public string Select3;

    public int CorrectNumber;

    public string AnswerSentence;
}
