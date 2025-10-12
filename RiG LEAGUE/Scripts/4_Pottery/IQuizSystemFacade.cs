using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * コーディング担当：Pottery
 * 
 * このインターフェースを実装した具体クラスのインスタンスメソッドを呼び出すことでQuizの機能を使います。
 * この設計図にあるメソッドを呼び出すことで各処理が正しく動くようにお願いします。
 * あくまで窓口クラスの設計図なので、内部処理は別クラスに書いてもOKです。
 * 
 * DBについてはScriptableObjectとかでも管理しやすいやつならなんでもOKです。
 */

/// <summary>
/// クイズDBを操作するQuiz空間の窓口
/// </summary>
/// <author>西田琉</author>
public interface IQuizSystemFacade
{
    /// <summary>
    /// 任意のIDを指定してクイズインスタンスを取得する
    /// </summary>
    /// <param name="id">欲しい問題のID</param>
    /// <returns>IQuiz実装インスタンス</returns>
    public IQuiz GetQuizWithID(int id);
    /**
     * ↑引数のIDの問題がない時はエラーを吐くようにしてほしいです。
     */



    /**
     * Unity上では自分の名前のフォルダ内のみ触ること！！！
     * 
     * mainブランチは触らない！！！
     * 作業はdevelopブランチから切った個人ブランチで行う
     * 基本的にpullはdevelopブランチから、pushは個人ブランチへ
     * 完成したらdevelopブランチへのpull requestを送る
     * コードレビューが欲しいときは完成してなくてもpull requestしてOK
     */
}
