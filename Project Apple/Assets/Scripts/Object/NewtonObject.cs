using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonObject : MonoBehaviour
{
    Animator animator;
    Rigidbody[] ragdollRigid;

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    public void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        animator = GetComponentInChildren<Animator>();
        ragdollRigid = GetComponentsInChildren<Rigidbody>();

        ResetStage();
    }

    public void RigidFreezeNone()
    {
        animator.enabled = false;
        SetRagdollRigid(false);
    }


    public void ResetStage()
    {
        animator.enabled = true;

        SetRagdollRigid(true);

        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
    }
    
    public void SetRagdollRigid(bool state)
    {
        foreach(var ele in ragdollRigid)
        {
            ele.isKinematic = state;
        }
    }
}
