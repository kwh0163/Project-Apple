using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private List<StageButton> stageButtons;
    [SerializeField] private int countsInPage;

    private int currentPage = 0;

    public void Initialize()
    {
        prevButton.interactable = false;
        foreach (var ele in stageButtons)
            ele.Initialize();
        ResetStage();
    }

    public void OpenWindow()
    {
        ResetStage();
        window.SetActive(true);
    }
    public void CloseWindow()
    {
        ResetStage();
        window.SetActive(false);
    }

    public void OnPrevButton()
    {
        currentPage--;
        ResetStage();
    }
    public void OnNextButton()
    {
        currentPage++;
        ResetStage();
    }

    void ResetStage()
    {
        int maxPage = GameManager.Instance.Stage.StageList.StageCount / countsInPage;
        
        prevButton.interactable = currentPage != 0;
        nextButton.interactable = currentPage != maxPage;
        for (int i = 0; i < countsInPage; i++)
        {
            int currentIndex = currentPage * countsInPage + i;
            stageButtons[i].SetText((currentIndex + 1).ToString());
            if (currentIndex >= GameManager.Instance.Stage.StageList.StageCount)
            {
                stageButtons[i].gameObject.SetActive(false);
                continue;
            }
            else
                stageButtons[i].gameObject.SetActive(true);
            if (GameManager.Instance.Stage.StageList.GetStageData(currentIndex).isStageUnlocked)
                stageButtons[i].UnLock();
            else
                stageButtons[i].Lock();
        }
    }
    public void OnButton(int index)
    {
        CloseWindow();
        GameManager.Instance.StartStage(index + currentPage * countsInPage);
    }
}
