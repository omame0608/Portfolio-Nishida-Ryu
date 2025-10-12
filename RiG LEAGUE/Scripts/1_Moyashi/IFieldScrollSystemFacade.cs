using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/**
 * コーディング担当：もやし
 * 
 * このインターフェースを実装した具体クラスのインスタンスメソッドを呼び出すことでスクロールの機能を使います。
 * この設計図にあるメソッドを呼び出すことで各処理が正しく動くようにお願いします。
 * あくまで窓口クラスの設計図なので、内部処理は別クラスに書いてもOKです。
 * 
 * フィールドは画面かなり奥で生成され、一定のスピードで手前方向(Z軸マイナス方向)にスクロール、
 * カメラの画角から出るくらい手前までスクロールしたら破棄するようにしてください。
 * 生成するZ座標、スピード、破棄するZ座標はインスペクター上から変更できるようにしてください。
 * またフィールドには並べられた8本のレールと、ランダムな位置にランダムな木オブジェクトを配置してください。
 */

/// <summary>
/// フィールドを動かすFieldScroll空間の窓口
/// </summary>
/// <author>西田琉</author>
public interface IFieldScrollSystemFacade
{
    /// <summary>
    /// フィールドのスクロールを開始する
    /// 加速度あり設定なら、ゆっくりとスクロールスピードになる
    /// 加速度なし設定なら、瞬時にスクロールスピードになる
    /// </summary>
    /// <param name="useAcceleration">加速度あり/なし設定</param>
    public void StartFieldScroll(bool useAcceleration);
    /**
     * スクロール中に呼ばれたらエラー
     */


    /// <summary>
    /// フィールドのスクロールを停止する
    /// 加速度あり設定なら、ゆっくりと停止する
    /// 加速度なし設定なら、瞬時に停止する
    /// </summary>
    /// <param name="useAcceleration">加速度あり/なし設定</param>
    public void StopFieldScroll(bool useAcceleration);
    /**
     * 停止中に呼ばれたらエラー
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
