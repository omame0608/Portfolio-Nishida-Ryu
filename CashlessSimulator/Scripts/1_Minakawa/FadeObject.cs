using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    private CanvasGroup canvas;
    public static bool Fade;

    // Start is called before the first frame update
    void Start()
    {
        canvas = this.GetComponent<CanvasGroup>();
        Fade = true;
        if (canvas != null) Debug.Log("canvas ok");
    }

    //fade‚Étrue‚ª“n‚³‚ê‚½‚Æ‚«ˆÃ“]
    public void SwitchFade(bool fade)
    {
        Fade = fade;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("fade updating");
        if (!Fade && canvas.alpha > 0)
        {
            canvas.alpha -=0.05f;
        }
        if (Fade && canvas.alpha < 1)
        {
            canvas.alpha +=0.05f;
        }
    }
}