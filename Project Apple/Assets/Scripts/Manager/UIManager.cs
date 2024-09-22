using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StageUI Stage { get; private set; }

    public void Initialize()
    {
        Stage = FindObjectOfType<StageUI>();

        Stage.Initialize();
    }
}
