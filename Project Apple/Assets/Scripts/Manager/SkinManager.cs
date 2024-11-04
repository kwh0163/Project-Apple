using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SkinManager : MonoBehaviour
{
    [SerializeField] AppleSkinEnum[] appleSkinEnums;
    [SerializeField] AppleData[] appleSkinData;
    public Dictionary<AppleSkinEnum, AppleData> AppleSkinList { get; private set; }
    public AppleSkinEnum CurrentAppleSkin { get; private set; }
    public UnityEvent<AppleData> ChangeSkinEvent { get; private set; }

    public void Initialize()
    {
        ChangeSkinEvent = new UnityEvent<AppleData>();

        AppleSkinList = new Dictionary<AppleSkinEnum, AppleData>();
        for(int i = 0; i < appleSkinEnums.Length; i++)
            AppleSkinList.Add(appleSkinEnums[i], appleSkinData[i]);
        CurrentAppleSkin = AppleSkinEnum.Apple;
    }
    public void SyncAppleSkin(AppleSkinEnum skinEnum, bool isUnlocked, bool isEquipped)
    {
        if (isEquipped)
            CurrentAppleSkin = skinEnum;
        AppleSkinList[skinEnum].IsUnlocked = isUnlocked;
        AppleSkinList[skinEnum].IsEquiped = isEquipped;
    }
    public void SetAppleSkin(AppleSkinEnum skinEnum)
    {
        AppleSkinList[CurrentAppleSkin].IsEquiped = false;
        GameManager.Instance.Prefs.EquipSkin(AppleSkinList[skinEnum]);
        CurrentAppleSkin = skinEnum;
        AppleSkinList[CurrentAppleSkin].IsEquiped = true;
        ChangeSkinEvent.Invoke(AppleSkinList[skinEnum]);
        GameManager.Instance.ResetMainMenu();
    }
    public AppleData GetCurrentAppleSkin(){
        return AppleSkinList[CurrentAppleSkin];
    }
    public void UnlockAppleSkin(AppleSkinEnum skinEnum)
    {
        GameManager.Instance.Prefs.UnlockSkin(AppleSkinList[skinEnum]);
        AppleSkinList[skinEnum].IsUnlocked = true;
    }
}
