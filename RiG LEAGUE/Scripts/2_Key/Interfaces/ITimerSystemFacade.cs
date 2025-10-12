using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * コーディング担当：key
 * 
 * このインターフェースを実装した具体クラスのインスタンスメソッドを呼び出すことでTimerの機能を使います。
 * この設計図にあるメソッドを呼び出すことで各処理が正しく動くようにお願いします。
 * あくまで窓口クラスの設計図なので、内部処理は別クラスに書いてもOKです。
 * 
 * UIについては、画面左上端にタイマーが表示されるようにしてください。
 * UIのサイズやイラスト等を後から変更する可能性大なので気を付けてください。
 */

/// <summary>
/// クイズの制限時間を管理するTimer空間の窓口
/// </summary>
/// <author>西田琉</author>
public interface ITimerSystemFacade
{
    /// <summary>
    /// タイマーからのタイムアップ通知を受け取るインスタンスを登録する
    /// </summary>
    /// <param name="timerObserver">登録するインスタンス</param>
    public void AddTimerObserver(ITimerObserver timerObserver);


    /// <summary>
    /// 登録済みの通知を受け取るインスタンスを削除する
    /// </summary>
    /// <param name="timerObserver">削除するインスタンス</param>
    public void RemoveTimerObserver(ITimerObserver timerObserver);
    /**
     * 登録されてなかったらエラー
     */


    /// <summary>
    /// 指定した秒数でカウントダウンを利用する
    /// カウントダウン終了時、登録されているすべてのインスタンスに通知を送る
    /// </summary>
    /// <param name="time">カウントする秒数</param>
    public void UseTimer(int time);
    /**
     * ↑登録されるITimerObserverにOnTimeUp()が宣言されてるので呼んであげてください
     * またカウントダウン中にこのメソッドが呼ばれたら、何もせずエラーを返してくれると助かります(なくてもOK)
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