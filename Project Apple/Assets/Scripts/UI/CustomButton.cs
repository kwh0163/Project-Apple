using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Sprite clickedSprite;
    Sprite defaultSprite;
    
    private Image image;

    public void Initialize()
    {
        image = GetComponent<Image>();
        defaultSprite = image.sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = clickedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = defaultSprite;
    }
}