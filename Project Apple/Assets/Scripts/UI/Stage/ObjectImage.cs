using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectImage : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject moveableObject;
    public GameObject GetObject { get { return moveableObject; } }
    private Text countText;
    private Image image;

    private int currentCount;

    public void Initialize()
    {
        currentCount = 0;
        countText = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }
    public void AddObject()
    {
        if (currentCount == 0)
            gameObject.SetActive(true);
        currentCount++;
        SetCount();
    }
    public void UseObject()
    {
        currentCount--;
        SetCount();
        if (currentCount == 0)
            gameObject.SetActive(false);
    }
    public void SetCount()
    {
        countText.text = currentCount.ToString();
    }
    public void ResetImage()
    {
        currentCount = 0;
        SetCount();
        gameObject.SetActive(false);
    }
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UseObject();
        GameManager.Instance.UI.Stage.Area.CopySelectedObject(moveableObject);
    }
}
