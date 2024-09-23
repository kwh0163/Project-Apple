using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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
    }
}
