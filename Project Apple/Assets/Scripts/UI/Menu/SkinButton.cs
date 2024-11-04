using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private AppleSkinEnum appleSkinEnum;
    public AppleSkinEnum SkinEnum => appleSkinEnum;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private GameObject equipButton;

    private SkinSelect origin;

    public void Initialize(SkinSelect _origin)
    {
        origin = _origin;
    }
    public void SetLock(bool isLock)
    {
        unlockButton.SetActive(isLock);
    }
    public void SetEquip(bool isEquipped)
    {
        equipButton.SetActive(!isEquipped);
    }
    public void OnEquip()
    {
        GameManager.Instance.Skin.SetAppleSkin(SkinEnum);
        origin.UpdateImage();
    }
    public void OnUnlock()
    {
        GameManager.Instance.Skin.UnlockAppleSkin(SkinEnum);
        origin.UpdateImage();
    }
}
