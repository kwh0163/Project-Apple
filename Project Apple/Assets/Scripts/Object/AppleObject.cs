using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleObject : MovableObject
{
    private Rigidbody rigid;
    public Rigidbody Rigid => rigid;

    private Vector3 velocity;
    private bool isEnd = false;

    public Block PrevBlock;

    private void Update()
    {
        if (!isEnd)
            velocity = rigid.velocity;
    }

    public override void Initialize()
    {
        base.Initialize();

        rigid = GetComponent<Rigidbody>();

        PrevBlock = null;

        SetFreeze();
    }

    public void SetVelocity(Vector3 vel)
    {
        rigid.velocity = vel;
    }

    public void AddForce(Vector3 direction, ForceMode force = ForceMode.Force)
    {
        rigid.AddForce(direction, force);
    }

    public override void PlayStage()
    {
        rigid.useGravity = true;
        rigid.constraints = RigidbodyConstraints.None;
    }

    public override void ResetStage()
    {
        base.ResetStage();

        SetFreeze();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.CompareTag("Newton"))
        {
            if (GameManager.Instance.Stage.CurrentState != StageState.Play)
                return;
            if (isEnd)
                return;
            isEnd = true;
            GameManager.Instance.Stage.EndStage();
            NewtonObject newton = collision.collider.GetComponentInParent<NewtonObject>();
            newton.RigidFreezeNone();
            collision.collider.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    void SetFreeze()
    {
        isEnd = false;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
