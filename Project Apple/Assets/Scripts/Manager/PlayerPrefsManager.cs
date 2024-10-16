using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public void Initialize()
    {
        string initializeKey = "IsInitialized";
        int count = GameManager.Instance.Stage.StageList.StageCount;
        if (PlayerPrefs.HasKey(initializeKey))
        {
            for (int i = 0; i < count; i++)
            {
                if (IsStageCleared(i))
                    GameManager.Instance.Stage.StageList.ClearStage(i);
                else
                    break;
            }
            return;
        }

        PlayerPrefs.SetInt(initializeKey, 1);

        for (int i = 0; i < count; i++)
            PlayerPrefs.SetInt(StageToKey(i), 0);
        Debug.Log(PlayerPrefs.GetInt(StageToKey(1)));
    }
    public void ClearStage(int stageNum)
    {
        if (!PlayerPrefs.HasKey(StageToKey(stageNum)))
        {
            Debug.Log("No Key");
            return;
        }
        PlayerPrefs.SetInt(StageToKey(stageNum), 1);
    }
    bool IsStageCleared(int stageNum)
    {
        if (!PlayerPrefs.HasKey(StageToKey(stageNum)))
        {
            Debug.Log("No Key");
            return false;
        }    
        return PlayerPrefs.GetInt(StageToKey(stageNum)) == 1;
    }
    string StageToKey(int stageNum)
    {
        return "Stage" + (stageNum + 1).ToString();
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
