using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public BuildArea Area { get; private set; }

    public void Initialize()
    {
        Area = FindObjectOfType<BuildArea>();

        Area.Initialize();
    }

    public void SetActive(bool isActive)
    {
        Area.gameObject.SetActive(isActive);
        gameObject.SetActive(isActive);
    }
}
