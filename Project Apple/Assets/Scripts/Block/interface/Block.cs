using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MovableObject
{
    public Rigidbody Rigid { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        Rigid = GetComponent<Rigidbody>();
    }
    public override void PlayStage()
    {
        Rigid.isKinematic = true;
    }
    public override void ResetStage()
    {
        base.ResetStage();
        Rigid.isKinematic = false;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.CompareTag("Apple"))
        {
            if (GameManager.Instance.Stage.CurrentState == StageState.Play)
                OnAppleEnter(collision);
        }
    }
    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
        if (collision.collider.CompareTag("Apple"))
        {
            if (GameManager.Instance.Stage.CurrentState == StageState.Play)
                OnAppleExit(collision);
        }
    }
    protected virtual void OnDestroy()
    {
        
    }

    protected virtual void OnAppleEnter(Collision collision)
    {
        collision.collider.GetComponent<AppleObject>().PrevBlock = this;
    }
    protected virtual void OnAppleExit(Collision collision)
    {

    }
}
