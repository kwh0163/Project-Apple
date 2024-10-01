using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleObject : MonoBehaviour
{
    private Rigidbody rigid;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private Vector3 velocity;
    private bool isEnd = false;
    private void FixedUpdate()
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

    public void AddForce(Vector3 direction)
    {
        rigid.AddForce(direction);
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
        transform.rotation = defaultRotation;
        transform.position = defaultPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            GameManager.Instance.Stage.SetStage();
        }
        else if (collision.collider.CompareTag("Newton"))
        {
            isEnd = true;
            GameManager.Instance.Stage.EndStage();
            NewtonObject newton = collision.collider.GetComponentInParent<NewtonObject>();
            newton.RigidFreezeNone();
            collision.collider.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
