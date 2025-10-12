using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 各プレイヤー用の回答確認表示を制御するクラス
/// </summary>
/// <author>西田琉</author>
public class AnswerCheckPanel : MonoBehaviour
{
    //参照一覧
    [SerializeField] private Text _answerText; //テキスト


    /// <summary>
    /// 回答をUIにセットする
    /// </summary>
    /// <param name="answerNumber">1,2,3のいずれか</param>
    public void SetAnswerNumber(int answerNumber)
    {
        if (answerNumber <= 0 || answerNumber >= 4) return;
        _answerText.text = answerNumber.ToString();
    }
}
