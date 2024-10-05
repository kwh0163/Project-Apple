using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    CustomButton[] buttons;
    public void Initialize()
    {
        buttons = GetComponentsInChildren<CustomButton>();
        foreach (var ele in buttons)
            ele.Initialize();
    }

    public void SetActice(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
