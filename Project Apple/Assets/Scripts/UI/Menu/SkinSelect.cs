using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinSelect : MonoBehaviour, IPointerDownHandler
{
    private Dictionary<AppleSkinEnum, SkinButton> skinButtonList;

    bool isOpened;

    public void Initialize()
    {
        isOpened = false;
        skinButtonList = new();
        foreach (var ele in GetComponentsInChildren<SkinButton>())
        {
            skinButtonList.Add(ele.SkinEnum, ele);
            ele.Initialize(this);
        }
        UpdateImage();
    }

    public void Open()
    {
        if (isOpened)
        {
            Close();
            return;
        }
        gameObject.SetActive(true);
        UpdateImage();
        isOpened = true;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        isOpened = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Close();
    }

    public void UpdateImage()
    {
        foreach (var ele in GameManager.Instance.Skin.AppleSkinList) {
            skinButtonList[ele.Key].SetLock(!ele.Value.IsUnlocked);
            skinButtonList[ele.Key].SetEquip(ele.Value.IsEquiped);
        }
    }
}
