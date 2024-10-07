using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public BuildArea Area { get; private set; }
    public BlockSelect Select { get; private set; }

    public void Initialize()
    {
        Area = FindObjectOfType<BuildArea>();
        Select = GetComponentInChildren<BlockSelect>();

        Area.Initialize();
        Select.Initialize();
    }

    public void SetActive(bool isActive)
    {
        Area.gameObject.SetActive(isActive);
        gameObject.SetActive(isActive);
    }
    public void PlayStage()
    {
        Area.gameObject.SetActive(false);
        Select.SetActive(false);
    }
    public void ResetGame()
    {
        Area.gameObject.SetActive(true);
        Select.SetActive(true);
        Select.ResetImages();
    }
}
