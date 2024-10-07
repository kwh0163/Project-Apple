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
    private Button button;

    public void Initialize()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        defaultSprite = image.sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(button.interactable)
            image.sprite = clickedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(button.interactable)
            image.sprite = defaultSprite;
    }
}