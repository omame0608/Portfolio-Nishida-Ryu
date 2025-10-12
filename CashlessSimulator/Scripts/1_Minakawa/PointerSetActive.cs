using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIControllerLaserPointer;

public class PointerSetActive : MonoBehaviour
{
    OVRPointerVisualizer pointer;
    // Start is called before the first frame update
    void Start()
    {
        pointer = GetComponent<OVRPointerVisualizer>();
        Debug.Log(pointer);
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeObject.Fade)
        {
            pointer.gameObject.SetActive(true);
        }
        else
        {
            pointer.gameObject.SetActive(false);
        }
    }
}
