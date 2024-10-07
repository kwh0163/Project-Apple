using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] GameObject lockedObject;
    private Text text;
    private Button button;

    public void Initialize()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
    }

    public void Lock()
    {
        lockedObject.SetActive(true);
        button.interactable = false;
    }
    public void UnLock()
    {
        lockedObject.SetActive(false);
        button.interactable = true;
    }
    public void SetText(string number)
    {
        text.text = number;
    }
}
