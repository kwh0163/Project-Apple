using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public MainMenuManager Menu { get; private set; }
    public StageManager Stage { get; private set; }
    public UIManager UI { get; private set; }

    private void Awake()
    {
        instance = this;

        Initialize();
    }

    void Initialize()
    {
        Stage = GetComponentInChildren<StageManager>();
        Stage.Initialize();

        UI = GetComponentInChildren<UIManager>();
        UI.Initialize();

        Menu = GetComponentInChildren<MainMenuManager>();
        Menu.Initialize();

        GoToMainMenu();
    }

    public void StartStage(int stageNumber)
    {
        Menu.StopCoroutine();
        UI.Menu.SetActice(false);
        UI.Stage.SetActive(true);
        UI.Stage.Select.SetImages(Stage.StageList.GetStageData(stageNumber));
        Stage.InstantiateStage(stageNumber);
    }

    public void GoToMainMenu()
    {
        UI.Stage.SetActive(false);
        Stage.InstantiateMenuStage();
        Menu.StartMenu();
        UI.Menu.SetActice(true);
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
