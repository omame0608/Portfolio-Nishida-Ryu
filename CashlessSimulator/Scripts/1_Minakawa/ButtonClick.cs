using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    private FadeObject canvas;
    public bool IsEnd;
    // Start is called before the first frame update
    void Start()
    {
        canvas = this.gameObject.transform.parent.gameObject.GetComponent < FadeObject > ();
    }

    public void Onclick()
    {
        if (IsEnd)
        {
            SceneManager.LoadScene("NishidaScene");
        }
        else
        {
            //スタートボタンがクリックされたときの処理
            canvas.SwitchFade(fade: false);
        }
    }
}
