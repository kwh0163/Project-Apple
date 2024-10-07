using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelect : MonoBehaviour
{
    [SerializeField] private RectTransform selectRect;
    [SerializeField] private Vector3 selectTargetPos;
    [SerializeField] private RectTransform buttonRect;
    [SerializeField] private Vector3 buttonTargetRot;

    [SerializeField] private GameObject selectObject;
    [SerializeField] private List<BlockType> blockKey;
    [SerializeField] private List<BlockImage> blockValue;

    [SerializeField] private float openTime;

    private Dictionary<BlockType, BlockImage> blockImages;

    StageBlockData currentData;

    bool isOpening = false;
    bool isOpened = true;
    Vector3 selectWindowDefaultPosition;
    Vector3 buttonImageDefaultRotation;

    public void Initialize()
    {
        blockImages = new();
        for (int i = 0; i < blockKey.Count; i++)
        {
            blockValue[i].Initialize();
            blockImages.Add(blockKey[i], blockValue[i]);
        }
        selectWindowDefaultPosition = selectRect.anchoredPosition;
        buttonImageDefaultRotation = buttonRect.rotation.eulerAngles;
    }

    public void UseBlock(BlockType block)
    {
        blockImages[block].UseBlock();
    }
    public void RemoveBlock(BlockType block)
    {
        blockImages[block].AddBlock();
    }
    public void SetImages(StageBlockData blockData)
    {
        currentData = blockData;
        foreach (var ele in blockValue)
            ele.ResetImage();
        foreach (var ele in blockData.BlockData)
            blockImages[ele].AddBlock();
    }
    public void ResetImages()
    {
        selectRect.anchoredPosition = selectWindowDefaultPosition;
        buttonRect.rotation = Quaternion.Euler(buttonImageDefaultRotation);
        isOpened = true;
        isOpening = false;
        foreach (var ele in blockValue)
            ele.ResetImage();
        foreach(var ele in currentData.BlockData)
            blockImages[ele].AddBlock();
    }
    public void SetActive(bool isActive)
    {
        selectObject.SetActive(isActive);
    }
    public void OnButton()
    {
        if (isOpening)
            return;
        if (isOpened)
            StartCoroutine(Closing());
        else
            StartCoroutine(Opening());

    }

    IEnumerator Opening()
    {
        isOpening = true;
        float timeCounter = 0;
        while(timeCounter <= openTime)
        {
            timeCounter += Time.deltaTime;
            selectRect.anchoredPosition = Vector3.Lerp( selectTargetPos, selectWindowDefaultPosition, Mathf.Clamp(timeCounter / openTime, 0, 1));
            buttonRect.rotation = Quaternion.Euler(Vector3.Lerp(buttonTargetRot, buttonImageDefaultRotation, Mathf.Clamp(timeCounter / openTime, 0, 1)));

            yield return null;
        }
        isOpening = false;
        isOpened = true;
        yield return null;
    }
    IEnumerator Closing()
    {
        isOpening = true;
        float timeCounter = 0;
        while (timeCounter <= openTime)
        {
            timeCounter += Time.deltaTime;
            selectRect.anchoredPosition = Vector3.Lerp(selectWindowDefaultPosition, selectTargetPos, Mathf.Clamp(timeCounter / openTime, 0, 1));
            
            buttonRect.rotation = Quaternion.Euler(Vector3.Lerp(buttonImageDefaultRotation, buttonTargetRot, Mathf.Clamp(timeCounter / openTime, 0, 1)));
            yield return null;
        }
        isOpening = false;
        isOpened = false;
        yield return null;
    }
}
