using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    [SerializeField] private InputField input;
    
    public void UnlockStage()
    {
        if(int.TryParse(input.text, out int stageNumber))
        {
            GameManager.Instance.Stage.StageList.UnlockStage(stageNumber);
        }
    }
}
