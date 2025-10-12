using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizSystemFacade : MonoBehaviour,IQuizSystemFacade
{
    public List<QuizProperty> QuizList = new List<QuizProperty>();　//クイズデータをリスト化

    /// <summary>
    /// idに合致するクイズデータを取得
    /// </summary>
    /// <param name="id">取り出すクイズデータのid</param>
    /// <returns>取り出すクイズデータ</returns>
    public IQuiz GetQuizWithID(int id)
    {
        Quiz QuizSet = new Quiz();　//空のクイズセットを作成

        foreach(var quizData in QuizList)
        {
            if(quizData.QuizID == id)　//idと合致したクイズデータの情報をQuizSetにセット
            {
                QuizSet.SetQuizData(quizData);
                return QuizSet;
            }
        }
        Debug.LogError("idに合致する問題が見つかりませんでした");
        return null;    //見つからなければnullを返す
    }

}
