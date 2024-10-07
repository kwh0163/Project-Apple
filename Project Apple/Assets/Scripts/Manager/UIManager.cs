using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public StageUI Stage { get; private set; }
    public MenuUI Menu { get; private set; }
    CustomButton[] buttons;
    public void Initialize()
    {
        buttons = canvas.GetComponentsInChildren<CustomButton>();
        foreach (var ele in buttons)
            ele.Initialize();

        Stage = FindObjectOfType<StageUI>();
        Stage.Initialize();
        Stage.SetActive(false);

        Menu = FindObjectOfType<MenuUI>();
        Menu.Initialize();
    }

    
}
