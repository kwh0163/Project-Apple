using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private GameObject quitButton;
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
        optionButton.SetActive(isActive);
        quitButton.SetActive(isActive);
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
    public void ShowButton()
    {
        startButton.SetActive(true);
        resetButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
    }
    public void HideButton()
    {
        startButton.SetActive(false);
        resetButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(false);
    }
}
