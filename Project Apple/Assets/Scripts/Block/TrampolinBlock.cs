using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinBlock : Block
{
    [SerializeField] private float bounciness;

    Coroutine currentCoroutine;
    bool isCalled;
    public override void Initialize()
    {
        base.Initialize();

        isCalled = false;
        onAppleEnterEvent.AddListener(ReflectApple);
    }

    void ReflectApple(Collision collision)
    {
        if (!isCalled)
            currentCoroutine = StartCoroutine(ReflectCoroutine(collision));
    }
    IEnumerator ReflectCoroutine(Collision collision)
    {
        isCalled = true;
        AppleObject apple = collision.collider.GetComponent<AppleObject>();
        Debug.Log(collision.impulse.magnitude);
        float yForce = collision.impulse.magnitude * (1f / apple.Rigid.mass) * bounciness;
        apple.AddForce(new Vector3(0, yForce, 0), ForceMode.VelocityChange);
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
