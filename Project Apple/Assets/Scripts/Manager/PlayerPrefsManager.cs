using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    string versionKey = "Version";
    [SerializeField] string currentVersion;
    public void Initialize()
    {
        InitializeStage();
        InitializeSound();
        InitializeAppleSkin();
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
    void InitializeStage()
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
        for (int i = 0; i < count; i++)
        {
            if (IsStageCleared(i))
                GameManager.Instance.Stage.StageList.ClearStage(i);
        }
    }
    bool IsStageCleared(int stageNum)
    {
        if (!PlayerPrefs.HasKey(StageToKey(stageNum)))
        {
            return false;
        }    
        return PlayerPrefs.GetInt(StageToKey(stageNum)) == 1;
    }
    string StageToKey(int stageNum)
    {
        return "Stage" + (stageNum + 1).ToString();
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
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    void InitializeSound()
    {
        if (!PlayerPrefs.HasKey("BGM"))
            PlayerPrefs.SetFloat("BGM", 0.7f);
        if (!PlayerPrefs.HasKey("SFX"))
            PlayerPrefs.SetFloat("SFX", 0.7f);
    }
    public void SetVolume(MixerType mixerType, float volume)
    {
        if (mixerType == MixerType.BGM)
            PlayerPrefs.SetFloat("BGM", volume);
        else if (mixerType == MixerType.SFX)
            PlayerPrefs.SetFloat("SFX", volume);
    }
    void InitializeAppleSkin()
    {
        bool isFirst = true;
        foreach(var ele in GameManager.Instance.Skin.AppleSkinList)
        {
            string key = "IsUnlockedAppleSkin" + ele.Value.Key; 
            if (!PlayerPrefs.HasKey(key))
                PlayerPrefs.SetInt(key, isFirst ? 1 : 0);
            bool isUnlocked = PlayerPrefs.GetInt(key) == 1;

            if (!PlayerPrefs.HasKey("AppleSkin"))
                PlayerPrefs.SetString("AppleSkin", ele.Value.Key);
            bool isEquipped = PlayerPrefs.GetString("AppleSkin") == ele.Value.Key;

            GameManager.Instance.Skin.SyncAppleSkin(ele.Key, isUnlocked, isEquipped);
            isFirst = false;
        }
    }
    public void EquipSkin(AppleData apple)
    {
        PlayerPrefs.SetString("AppleSkin", apple.Key);
    }
    public void UnlockSkin(AppleData apple)
    {
        string key = "IsUnlockedAppleSkin" + apple.Key;
        PlayerPrefs.SetInt(key, 1);
    }
}
