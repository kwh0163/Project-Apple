using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : TriggerBlock
{
    [SerializeField] bool isTriggered;

    public override void Initialize()
    {
        base.Initialize();
        isTriggered = false;
    }
    public override void ResetStage()
    {
        base.ResetStage();
        isTriggered = false;
    }

    protected override void OnAppleEnter(Collision collision)
    {
        if (isTriggered)
            return;
        isTriggered = true;
        base.OnAppleEnter(collision);
    }
}
