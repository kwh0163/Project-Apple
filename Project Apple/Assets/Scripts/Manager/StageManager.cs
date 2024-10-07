using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IManager
{
    [SerializeField] private Transform stageRootTransform;
    public ObjectManager StageObject { get; private set; }
    public StageListManager StageList { get; private set; }
    public StageState CurrentState;
    public Transform BlockParentTransform { get; private set; }

    public void Initialize()
    {
        StageList = GetComponent<StageListManager>();
        StageList.Initialize();
        StageObject = GetComponent<ObjectManager>();
    }

    public void ResetStage()
    {
        CurrentState = StageState.Prepare;

        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.ResetGame();
    }

    public void SetStage()
    {
        if (CurrentState == StageState.End)
            return;

        CurrentState = StageState.Prepare;
        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.Area.SetActive(true);
    }

    public void PlayStage()
    {
        if (CurrentState == StageState.End)
            return;

        if (CurrentState == StageState.Play)
            return;
        CurrentState = StageState.Play;
        GameManager.Instance.UI.Stage.PlayStage();

        StageObject.PlayApple();

    }

    public void EndStage()
    {
        CurrentState = StageState.End;
    }
    public void InstantiateMenuStage()
    {
        CurrentState = StageState.Play;
        if(stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        GameObject stage = Instantiate(StageList.MainStage, stageRootTransform);

        StageObject.Initialize(stage);
    }

    public void InstantiateStage(int stageNumber)
    {
        CurrentState = StageState.Prepare;
        if (stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        GameObject stage = Instantiate(StageList.GetStage(stageNumber), stageRootTransform);

        BlockParentTransform = stage.transform;

        StageObject.Initialize(stage);

    }
}
