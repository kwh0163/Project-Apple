using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageListManager : MonoBehaviour
{
    [SerializeField] private GameObject mainStage;
    public GameObject MainStage { get { return mainStage; } }

    [SerializeField] private List<GameObject> stageList;

    public GameObject GetStage(int stageNumber)
    {
        return stageList[stageNumber];
    }
}
