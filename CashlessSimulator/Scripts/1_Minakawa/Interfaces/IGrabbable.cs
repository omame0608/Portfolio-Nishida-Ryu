using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コントローラで保持出来るオブジェクト
public interface IGrabbable
{
    //つかまれたときの処理
    public bool OnGrab(Transform controller);

    //離されたときの処理
    public void OnRelease();
}
