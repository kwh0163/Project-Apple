using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
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
        Clear.Close();
    }
    public void PlayStage()
    {
        startButton.SetActive(false);
        Area.gameObject.SetActive(false);
        Select.SetActive(false);
    }
    public void ResetGame()
    {
        startButton.SetActive(true);
        Area.gameObject.SetActive(true);
        Select.SetActive(true);
        Select.ResetImages();
        Clear.Close();
    }
}
