using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageState
{
    Prepare,
    Play,
    End
}

public class StageManager : MonoBehaviour, IManager
{
    [SerializeField] private Transform stageRootTransform;

    public ObjectManager StageObject { get; private set; }
    public StageListManager StageList { get; private set; }

    public StageState CurrentState;

    public void Initialize()
    {
        StageList = GetComponent<StageListManager>();
        StageObject = GetComponent<ObjectManager>();
    }

    public void ResetStage()
    {
        CurrentState = StageState.Prepare;

        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.Area.SetActice(true);
    }

    public void SetStage()
    {
        if (CurrentState == StageState.End)
            return;

        CurrentState = StageState.Prepare;
        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.Area.SetActice(true);
    }

    public void PlayStage()
    {
        if (CurrentState == StageState.End)
            return;

        if (CurrentState == StageState.Play)
            return;
        CurrentState = StageState.Play;
        GameManager.Instance.UI.Stage.Area.SetActice(false);

        StageObject.PlayApple();

    }

    public void EndStage()
    {
        CurrentState = StageState.End;
    }

    public void InstantiateMenuStage()
    {
        if(stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        Instantiate(StageList.MainStage, stageRootTransform);

        StageObject.Initialize();
    }

    public void InstantiateStage(int stageNumber)
    {
        if (stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        Instantiate(StageList.GetStage(stageNumber), stageRootTransform);

        StageObject.Initialize();
    }
}
