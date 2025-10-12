using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

//�����^�ׂ�I�u�W�F�N�g�ɃA�^�b�`���܂�
public class CrabbableObject : MonoBehaviour, IGrabbable
{
    private Rigidbody rigidbody;
    private Vector3 lastPosition;
    private float lastTime;
    private Vector3 velocity;

    public bool IsDrink;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        lastTime = Time.time;
        velocity = new Vector3(0f, 0f, 0f);
    }
    public bool OnGrab(Transform controller)
    {
        if (this.transform.parent == null)
        {
            this.transform.parent = controller;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnRelease()
    {
        this.transform.parent = null;
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;
    }
    void Update()
    {
        velocity = (this.transform.position - lastPosition) / (Time.time - lastTime);
        lastTime = Time.time;
        lastPosition = this.transform.position;
    }
}
