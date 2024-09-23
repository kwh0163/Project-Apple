using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleObject : MonoBehaviour
{
    [SerializeField] private float gravity;
    private Vector3 force;
    private Rigidbody rigid;
    private bool isPrepare = true;

    private Vector3 defaultPosition;

    public void Initialize()
    {
        defaultPosition = transform.position;
        force = new Vector3(0, gravity, 0);
        rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void PlayApple()
    {
        isPrepare = false;
        rigid.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public void ChangePositionToDefault()
    {
        isPrepare = true;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = defaultPosition;
    }

    void FixedUpdate()
    {
        if (isPrepare)
            return;
        // 사용자 정의 중력을 적용
        rigid.AddForce(force, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            GameManager.Instance.Stage.SetStage();
        }
        else if (collision.collider.CompareTag("Newton"))
        {
            GameManager.Instance.Stage.EndStage();
            NewtonObject newton = collision.collider.GetComponent<NewtonObject>();
            newton.RigidFreezeNone();
            newton.AddForce(rigid.velocity.normalized * 5f);
        }
    }
}
