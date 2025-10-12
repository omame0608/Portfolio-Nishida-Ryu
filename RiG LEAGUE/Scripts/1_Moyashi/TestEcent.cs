using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TestEcent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<FieldScrollSystem>().StartFieldScroll(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // マウスの左ボタンがクリックされたら
        {
            FindObjectOfType<FieldScrollSystem>().StopFieldScroll(true);
        }
    }
}
