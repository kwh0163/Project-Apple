using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBlock : Block
{
    [SerializeField] private float power;

    AppleObject prevApple;
    Coroutine currentCoroutine;
    bool isCalled;

    public override void Initialize()
    {
        base.Initialize();

        isCalled = false;
    }
    protected override void OnAppleEnter(Collision collision)
    {
        AppleObject collisionApple = collision.collider.GetComponent<AppleObject>();
        if(prevApple != collisionApple)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            isCalled = false;
        }
        if (!isCalled)
            currentCoroutine = StartCoroutine(ForceCoroutine(collisionApple));
        prevApple = collisionApple;
        base.OnAppleEnter(collision);
    }

    IEnumerator ForceCoroutine(AppleObject apple)
    {
        isCalled = true;
        Vector3 force = (IsFlipped ? Vector3.left : Vector3.right) * power;
        apple.AddForce(force, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1);
        isCalled = false;
        currentCoroutine = null;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }
}
