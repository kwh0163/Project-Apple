using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject resetButton;
    public BuildArea Area { get; private set; }
    public BlockSelect Select { get; private set; }

    public ClearUI Clear { get; private set; }

    public void Initialize()
    {
        Area = FindObjectOfType<BuildArea>();
        Select = GetComponent<BlockSelect>();
        Clear = GetComponent<ClearUI>();

        Area.Initialize();
        Select.Initialize();
        Clear.Initialize();
    }

    public void SetActive(bool isActive)
    {
        Area.gameObject.SetActive(isActive);
        Select.SetActive(isActive);
        gameObject.SetActive(isActive);
        startButton.SetActive(isActive);
        resetButton.SetActive(isActive);
        Clear.Close();
    }
    public void PlayStage()
    {
        startButton.SetActive(false);
        Select.SetActive(false);
    }
    public void EndStage()
    {
        resetButton.SetActive(false);
    }
}
