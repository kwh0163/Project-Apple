using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform stageRootTransform;
    public ObjectManager StageObject { get; private set; }
    public StageListManager StageList { get; private set; }
    public StageState CurrentState;
    public Transform BlockParentTransform { get; private set; }

    bool isMenu;
    int currentStageNumber;

    public void Initialize()
    {
        StageList = GetComponent<StageListManager>();
        StageList.Initialize();
        StageObject = GetComponent<ObjectManager>();
        StageObject.Initialize();
    }

    public void ResetStage()
    {
        CurrentState = StageState.Prepare;

        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.SetActive(true);
    }

    public void SetStage()
    {
        if (CurrentState == StageState.End)
            return;

        CurrentState = StageState.Prepare;
        StageObject.ResetObject();

        GameManager.Instance.UI.Stage.Clear.Close();
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

        StageObject.PlayStage();

    }

    public void EndStage()
    {
        CurrentState = StageState.End;
        if (isMenu)
            return;
        StageList.ClearStage(currentStageNumber);
        GameManager.Instance.UI.Stage.EndStage();
        GameManager.Instance.UI.Stage.Clear.Open();
    }
    public void InstantiateMenuStage()
    {
        isMenu = true;
        if(stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        GameObject stage = Instantiate(StageList.MenuStage, stageRootTransform);

        StageObject.SetStage(stage);
    }

    public void InstantiateStage(int stageNumber)
    {
        isMenu = false;
        currentStageNumber = stageNumber;

        CurrentState = StageState.Prepare;

        if (stageRootTransform.childCount > 0)
            Destroy(stageRootTransform.GetChild(0).gameObject);

        GameObject stage = Instantiate(StageList.GetStageData(currentStageNumber).StagePrefab, stageRootTransform);

        BlockParentTransform = stage.transform;

        StageObject.SetStage(stage);

    }
    public void OnExitButton()
    {
        ResetStage();
        GameManager.Instance.GoToMainMenu();
    }
    public void OnNextButton()
    {
        GameManager.Instance.StartStage(currentStageNumber + 1);
    }
}
