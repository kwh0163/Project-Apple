using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private SoundEnum[] enums;
    [SerializeField] private AudioClip[] clips;
    private Dictionary<SoundEnum, AudioClip> clipList;

    [SerializeField] private float maxVolume;
    [SerializeField] private float minVolume;

    public void Initialize()
    {
        clipList = new();
        bgmSource.Play();
        for(int i = 0; i < clips.Length; i++)
        {
            clipList.Add(enums[i], clips[i]);
        }
    }

    public void PlaySound(SoundEnum sound)
    {
        sfxSource.PlayOneShot(clipList[sound]);
    }

    public void SetBGM(float value)
    {
        bgmSource.volume = value;
    }
    public void SetSFX(float value)
    {
        sfxSource.volume = value;
    }
}
