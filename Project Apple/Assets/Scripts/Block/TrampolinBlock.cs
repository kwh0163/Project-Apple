using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinBlock : Block
{
    [SerializeField] private float bounciness;
    public override void Initialize()
    {
        base.Initialize();

        onAppleEnterEvent.AddListener(ReflectApple);
    }

    void ReflectApple(Collision collision)
    {
        AppleObject apple = collision.collider.GetComponent<AppleObject>();
        float yForce = collision.impulse.magnitude * (1f / apple.Rigid.mass) * bounciness;
        apple.AddForce(new Vector3(0, yForce, 0), ForceMode.VelocityChange);
    }
}
