using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : MonoBehaviour,IActionable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UserAction()
    {
        Debug.Log("Action saretayo");
        this.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
