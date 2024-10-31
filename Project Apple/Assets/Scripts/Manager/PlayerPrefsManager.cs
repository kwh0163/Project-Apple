using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    string versionKey = "Version";
    [SerializeField] string currentVersion;
    public void Initialize()
    {
        int count = GameManager.Instance.Stage.StageList.StageCount;
        if (!IsVersionMatched())
        {
            for (int i = 0; i < count; i++)
            {
                if (!PlayerPrefs.HasKey(StageToKey(i)))
                    PlayerPrefs.SetInt(StageToKey(i), 0);
            }
        }
        for(int i = 0; i < count; i++)
        {
            if (IsStageCleared(i))
                GameManager.Instance.Stage.StageList.ClearStage(i);
        }
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
    bool IsVersionMatched()
    {
        if (!PlayerPrefs.HasKey(versionKey) ||
            !(PlayerPrefs.GetString(versionKey) == currentVersion))
        {
            PlayerPrefs.SetString(versionKey, currentVersion);
            return false;
        }
        return true;
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
