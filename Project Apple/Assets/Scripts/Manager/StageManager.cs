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

    public List<Block> BlockList { get; private set; }

    StageState state;

    public void Initialize()
    {
        Apple = FindObjectOfType<AppleObject>();
        Newton = FindObjectOfType<NewtonObject>();
        BlockList = new List<Block>();

        foreach (var ele in FindObjectsOfType<Block>())
        {
            BlockList.Add(ele);
            ele.Initialize();
        }

        Apple.Initialize();
        Newton.Initialize();

        SetStage();
    }

    public void ResetStage()
    {
        state = StageState.Prepare;
        Apple.ResetStage();
        Newton.ResetStage();
        foreach (var ele in BlockList)
            ele.ResetStage();

        GameManager.Instance.UI.Stage.Area.SetActice(true);
    }

    public void SetStage()
    {
        if (state == StageState.End)
            return;

        state = StageState.Prepare;
        Apple.ResetStage();
        Newton.ResetStage();
        foreach (var ele in BlockList)
            ele.ResetStage();

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
