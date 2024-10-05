using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StageUI Stage { get; private set; }
    public MenuUI Menu { get; private set; }
    public void Initialize()
    {
        Stage = FindObjectOfType<StageUI>();
        Stage.Initialize();
        Stage.SetActive(false);

        Menu = FindObjectOfType<MenuUI>();
        Menu.Initialize();
    }
}
