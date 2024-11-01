using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private SoundEnum[] enums;
    [SerializeField] private AudioClip[] clips;
    private Dictionary<SoundEnum, AudioClip> clipList;

    public void Initialize()
    {
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
}
