using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListManager : MonoBehaviour
{
    [SerializeField] private GameObject mainStage;
    public GameObject MainStage { get { return mainStage; } }
    [SerializeField] private List<StageData> stageData;
    public int StageCount { get { return stageData.Count; } }
    public StageData GetStageData(int stageNumber)
    {
        if (stageNumber >= stageData.Count)
            return stageData[stageNumber - 1];
        return stageData[stageNumber];
    }
    public void Initialize()
    {
        stageData[0].isStageUnlocked = true;
        for (int i = 1; i < stageData.Count; i++)
            stageData[i].isStageUnlocked = false;
    }
    public void ClearStage(int currentStageNumber)
    {
        stageData[currentStageNumber].isStageCleared = true;
        if(stageData.Count > currentStageNumber + 1)
        {
            stageData[currentStageNumber + 1].isStageUnlocked = true;
        }
    }
}
