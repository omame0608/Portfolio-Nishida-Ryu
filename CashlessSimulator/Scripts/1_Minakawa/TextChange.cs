using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextChange : MonoBehaviour,IDisplayable
{
    private Text displayText=null;
    // Start is called before the first frame update
    void Start()
    {
        displayText=this.gameObject.GetComponent<Text>();
    }

    public void Display(string text)
    {
        if (displayText == null) return;
        displayText.text=text;
    }
}