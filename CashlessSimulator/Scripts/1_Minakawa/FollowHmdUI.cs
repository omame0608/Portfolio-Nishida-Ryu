using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHmdUI : MonoBehaviour
{
    [SerializeField] float x = 0f;
    [SerializeField] float y = 0f;
    [SerializeField] float z = 0f;

    private Transform hmdT;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        hmdT = GameObject.Find("CenterEyeAnchor").GetComponent<Transform>().transform;
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisRotate = -hmdT.rotation.eulerAngles.x * Mathf.Deg2Rad;
        float yAxisRotate = -hmdT.rotation.eulerAngles.y * Mathf.Deg2Rad;

        rectTransform.position = hmdT.position +
            new Vector3(
                x * (float)Math.Cos(yAxisRotate) - z * (float)Math.Sin(yAxisRotate),
                y,
                z * (float)Math.Cos(yAxisRotate) + x * (float)Math.Sin(yAxisRotate)
                );
        rectTransform.rotation = Quaternion.Euler(0f, hmdT.rotation.eulerAngles.y, 0f);

    }
}
