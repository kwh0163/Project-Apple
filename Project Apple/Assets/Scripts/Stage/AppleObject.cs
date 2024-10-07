using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleObject : MonoBehaviour
{
    private Rigidbody rigid;
    public Rigidbody Rigid { get { return rigid; } }

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private Vector3 velocity;
    private bool isEnd = false;
    private void Update()
    {
        if (!isEnd)
            velocity = rigid.velocity;
    }

    public void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        rigid = GetComponent<Rigidbody>();

        ResetStage();
    }

    public void SetVelocity(Vector3 vel)
    {
        rigid.velocity = vel;
    }

    public void AddForce(Vector3 direction, ForceMode force = ForceMode.Force)
    {
        rigid.AddForce(direction, force);
    }

    public void PlayApple()
    {
        rigid.useGravity = true;
        rigid.constraints = RigidbodyConstraints.None;
    }

    public void ResetStage()
    {
        isEnd = false;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Newton"))
        {
            isEnd = true;
            GameManager.Instance.Stage.EndStage();
            NewtonObject newton = collision.collider.GetComponentInParent<NewtonObject>();
            newton.RigidFreezeNone();
            collision.collider.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
