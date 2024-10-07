using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockImage : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Block block;
    public Block Block { get { return block; } }
    private Text countText;

    private int currentCount;

    private bool isClicked = false;

    public void Initialize()
    {
        currentCount = 0;
        countText = GetComponentInChildren<Text>();
    }
    public void AddBlock()
    {
        if (currentCount == 0)
            gameObject.SetActive(true);
        currentCount++;
        SetCount();
    }
    public void UseBlock()
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

    public void OnPointerDown(PointerEventData eventData)
    {
        UseBlock();
        GameManager.Instance.UI.Stage.Area.CopySelectedBlock(block);
    }
}
