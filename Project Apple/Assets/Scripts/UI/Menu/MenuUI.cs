using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public StageSelect Stage { get; private set; }
    public void Initialize()
    {
        Stage = GetComponent<StageSelect>();
        Stage.Initialize();
        Stage.CloseWindow();
    }

    public void SetActice(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
