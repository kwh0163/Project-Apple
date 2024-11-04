using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleObject : MovableObject
{
    private GameObject currentPrefab;

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

        currentPrefab = transform.GetChild(0).gameObject;
        rigid = GetComponent<Rigidbody>();

        PrevBlock = null;

        SetFreeze();
    }

    public void SetSkin(AppleData data)
    {
        Destroy(currentPrefab);
        currentPrefab = Instantiate(data.Prefab, transform);
        currentPrefab.transform.localScale = data.Scale;
        currentPrefab.transform.rotation = Quaternion.Euler(data.Rotation);
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
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    public override void ResetStage()
    {
        base.ResetStage();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
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
            rigid.constraints = RigidbodyConstraints.None;
            GameManager.Instance.Stage.EndStage();
            GameManager.Instance.Sound.PlaySound(SoundEnum.NewtonHit);
            NewtonObject newton = collision.collider.GetComponentInParent<NewtonObject>();
            newton.RigidFreezeNone();
            collision.collider.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    void SetFreeze()
    {
        isEnd = false;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
