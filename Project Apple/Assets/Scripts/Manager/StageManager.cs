using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum StageState
{
    Prepare,
    Play,
    Finish
}
public class StageManager : MonoBehaviour, IManager
{
    StageState currentState;

    public AppleObject Apple { get; private set; }

    public void Initialize()
    {
        Apple = GetComponentInChildren<AppleObject>();
    }

    public void SetStage()
    {

    }

}
