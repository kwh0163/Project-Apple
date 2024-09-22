using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public BuildArea Area { get; private set; }

    public void Initialize()
    {
        Area = GetComponentInChildren<BuildArea>();

        Area.Initialize();
    }
}
