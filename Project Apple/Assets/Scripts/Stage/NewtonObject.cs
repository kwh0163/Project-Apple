using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonObject : MonoBehaviour
{
    Rigidbody rigid;
    public void Initialize()
    {
        rigid = GetComponent<Rigidbody>();

        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void RigidFreezeNone()
    {
        rigid.constraints = RigidbodyConstraints.None;
    }

    public void AddForce(Vector3 force)
    {
        rigid.AddForce(force, ForceMode.Force);
    }
}
