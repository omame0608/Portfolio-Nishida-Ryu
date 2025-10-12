using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クイズ窓口のスタブ
/// </summary>
/// <author>西田琉</author>
public class StubQuizSystemFacade : IQuizSystemFacade
{
    public IQuiz GetQuizWithID(int id)
    {
        Debug.Log($"ID{id}の問題を返します");
        return new StubQuiz();
    }
}
