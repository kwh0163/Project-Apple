using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBlock : Block
{
    [SerializeField] private float power;

    Coroutine currentCoroutine;
    bool isCalled;

    public override void Initialize()
    {
        base.Initialize();

        isCalled = false;
        onAppleEnterEvent.AddListener(AddForce);
    }
    private void AddForce(Collision collision)
    {
        if (!isCalled)
            currentCoroutine = StartCoroutine(ForceCoroutine(collision));
    }

    IEnumerator ForceCoroutine(Collision collision)
    {
        isCalled = true;
        Vector3 force = (IsFlipped ? Vector3.left : Vector3.right) * power;
        Debug.Log(force);
        collision.collider.GetComponent<AppleObject>().AddForce(force, ForceMode.VelocityChange);
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
