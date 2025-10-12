using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{

    public float destroyZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < destroyZ)
            {
                Destroy(this.gameObject);
            }
        transform.Translate(Vector3.back * FieldScrollSystem.currentSpeed * Time.deltaTime);
    }
}
