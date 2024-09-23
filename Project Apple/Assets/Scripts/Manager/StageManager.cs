using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StageState
{
    Prepare,
    Play,
    End
}

public class StageManager : MonoBehaviour, IManager
{
    public AppleObject Apple { get; private set; }
    public NewtonObject Newton { get; private set; }

    StageState state;

    public void Initialize()
    {
        Apple = FindObjectOfType<AppleObject>();
        Newton = FindObjectOfType<NewtonObject>();

        Apple.Initialize();
        Newton.Initialize();

        SetStage();
    }

    public void SetStage()
    {
        if (state == StageState.End)
            return;

        state = StageState.Prepare;
        Apple.ChangePositionToDefault();
        GameManager.Instance.UI.Stage.Area.SetActice(true);
    }
    
    public void PlayStage()
    {
        if (state == StageState.End)
            return;

        if (state == StageState.Play)
            return;
        state = StageState.Play;
        GameManager.Instance.UI.Stage.Area.SetActice(false);
        Apple.PlayApple();

    }

    public void EndStage()
    {
        state = StageState.End;
    }
}
