using System.Collections;
using System.Collections.Generic;
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
        UI = GetComponentInChildren<UIManager>();
        UI.Initialize();

        Stage = GetComponentInChildren<StageManager>();
        Stage.Initialize();

        Menu = GetComponentInChildren<MainMenuManager>();
        Menu.Initialize();

        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        Stage.InstantiateMenuStage();
        Menu.StartMenu();
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
