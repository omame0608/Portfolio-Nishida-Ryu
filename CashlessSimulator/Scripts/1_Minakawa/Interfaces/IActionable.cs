using Meta.XR.InputActions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザからのアクションに反応できるもの
/// </summary>
public interface IActionable
{
    /// <summary>
    /// ユーザからのアクションに対する処理
    /// ユーザがクリックや決定ボタンでオブジェクトにアクションを起こすと、
    /// このメソッドが呼ばれるよう統一します
    /// </summary>
    public void UserAction();
}
