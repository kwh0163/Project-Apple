using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public StageSelect Stage { get; private set; }
    public SkinSelect Skin { get; private set; }
    public void Initialize()
    {
        Stage = GetComponent<StageSelect>();
        Stage.Initialize();
        Stage.CloseWindow();

        Skin = GetComponentInChildren<SkinSelect>();
        Skin.Initialize();
        Skin.Close();
    }

    public void SetActice(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
