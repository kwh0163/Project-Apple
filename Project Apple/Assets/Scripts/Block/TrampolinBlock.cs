using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinBlock : Block
{
    [SerializeField] private float bounciness;
    [SerializeField] private float continueJumpMult;

    Coroutine currentCoroutine;
    bool isCalled;
    public override void Initialize()
    {
        base.Initialize();

        isCalled = false;
    }

    protected override void OnAppleEnter(Collision collision)
    {
        if (!isCalled)
        {
            currentCoroutine = StartCoroutine(ReflectCoroutine());

            AppleObject apple = collision.collider.GetComponent<AppleObject>();
            float yForce = collision.impulse.magnitude * (1f / apple.Rigid.mass) * bounciness;
            if (apple.PrevBlock != null && apple.PrevBlock.Type == BlockType.Trampolin)
                yForce *= continueJumpMult;
            apple.AddForce(new Vector3(0, yForce, 0), ForceMode.VelocityChange);
        }
        base.OnAppleEnter(collision);
    }

    IEnumerator ReflectCoroutine()
    {
        isCalled = true;
        yield return new WaitForSeconds(0.1f);
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
