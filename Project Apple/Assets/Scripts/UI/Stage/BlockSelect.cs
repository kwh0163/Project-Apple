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
    [SerializeField] private List<ObjectType> blockKey;
    [SerializeField] private List<ObjectImage> blockValue;

    [SerializeField] private float openTime;

    [SerializeField] private ObjectImage appleImage;

    private Dictionary<ObjectType, ObjectImage> blockImages;

    StageData currentData;

    bool isOpening = false;
    bool isOpened = true;
    public bool IsOpened { get { return isOpened; } }
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

        SetAppleImage(GameManager.Instance.Skin.GetCurrentAppleSkin());
        GameManager.Instance.Skin.ChangeSkinEvent.AddListener(SetAppleImage);
    }

    public void UseObject(ObjectType type)
    {
        blockImages[type].UseObject();
    }
    public void RemoveObject(ObjectType type)
    {
        blockImages[type].AddObject();
    }
    public void SetImages(StageData blockData)
    {
        currentData = blockData;
        foreach (var ele in blockValue)
            ele.ResetImage();
        foreach (var ele in blockData.BlockData)
            blockImages[ele].AddObject();
    }
    public void ResetImages()
    {
        selectRect.anchoredPosition = selectWindowDefaultPosition;
        buttonRect.rotation = Quaternion.Euler(buttonImageDefaultRotation);
        isOpened = true;
        isOpening = false;
    //    foreach (var ele in blockValue)
    //        ele.ResetImage();
    //    foreach(var ele in currentData.BlockData)
    //        blockImages[ele].AddBlock();
    }
    private void SetAppleImage(AppleData apple)
    {
        appleImage.SetImage(apple.Sprite);
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
