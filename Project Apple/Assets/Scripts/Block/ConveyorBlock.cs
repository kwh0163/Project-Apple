using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlock : Block
{
    [SerializeField] private bool isRight = true;

    public override void Initialize()
    {
        base.Initialize();
    }

}
