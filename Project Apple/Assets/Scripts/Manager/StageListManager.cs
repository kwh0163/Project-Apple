using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListManager : MonoBehaviour
{
    [SerializeField] private GameObject mainStage;
    public GameObject MainStage { get { return mainStage; } }

    [SerializeField] private List<GameObject> stageList;
    public List<GameObject> StageList { get { return stageList; } }
    [SerializeField] private List<StageBlockData> blockDataList;
    public List<StageBlockData> BlockList { get { return blockDataList; } }
    [SerializeField] private List<bool> isStageClear = new ();

    public void Initialize()
    {
        isStageClear[0] = true;
        for (int i = 1; i < isStageClear.Count; i++)
            isStageClear[i] = false;
    }

    public GameObject GetStage(int stageNumber)
    {
        return stageList[stageNumber];
    }
    public bool IsStageClear(int stageNumber)
    {
        return isStageClear[stageNumber];
    }
}
