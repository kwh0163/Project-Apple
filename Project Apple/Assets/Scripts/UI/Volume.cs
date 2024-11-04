using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private MixerType mixerType;
    [SerializeField] private Image handleImage;
    private Slider slider;

    public void Initialize()
    {
        slider = GetComponent<Slider>();
        if (mixerType == MixerType.BGM)
            slider.value = PlayerPrefs.GetFloat("BGM");
        else if (mixerType == MixerType.SFX)
            slider.value = PlayerPrefs.GetFloat("SFX");
    }

    public void SetVolume()
    {
        float value = slider.value;
        if (value >= .75f)
            handleImage.sprite = sprites[0];
        else if (value >= .5f)
            handleImage.sprite = sprites[1];
        else if (value >= .25f)
            handleImage.sprite = sprites[2];
        else if (value > 0)
            handleImage.sprite = sprites[3];
        else
            handleImage.sprite = sprites[4];

        if (mixerType == MixerType.BGM)
        {
            GameManager.Instance.Prefs.SetVolume(MixerType.BGM, value);
            GameManager.Instance.Sound.SetBGM(value);
        }
        else if (mixerType == MixerType.SFX)
        {
            GameManager.Instance.Prefs.SetVolume(MixerType.SFX, value);
            GameManager.Instance.Sound.SetSFX(value);
        }
    }
}
