using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public StageUI Stage { get; private set; }
    public MenuUI Menu { get; private set; }
    public OptionUI Option { get; private set; }
    CustomButton[] buttons;
    public void Initialize()
    {
        SetUpCanvasScaler(1920, 1080);

        buttons = canvas.GetComponentsInChildren<CustomButton>();
        foreach (var ele in buttons)
            ele.Initialize();

        Stage = FindObjectOfType<StageUI>();
        Stage.Initialize();
        Stage.SetActive(false);

        Menu = FindObjectOfType<MenuUI>();
        Menu.Initialize();

        Option = FindObjectOfType<OptionUI>();
        Option.Initialize();
    }

    public void SetUpCanvasScaler(int setWidth, int setHeight)
    {
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(setWidth, setHeight);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
}
