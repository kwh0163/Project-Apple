using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject window;
    private Volume[] volumes;

    public void Initialize()
    {
        volumes = GetComponentsInChildren<Volume>();
        for (int i = 0; i < volumes.Length; i++)
            volumes[i].Initialize();
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Close();
    }
}
