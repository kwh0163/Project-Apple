#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private AudioClip [] clips;
    private int currentIdx;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        currentIdx = 0;
    }

    public void Play()
    {
        source.PlayOneShot(clips[currentIdx]);
        Debug.Log(clips[currentIdx].name);
    }
    public void Prev()
    {
        currentIdx--;
        if (currentIdx < 0)
            currentIdx = clips.Length - 1;
    }
    public void Next()
    {
        currentIdx++;
        if (currentIdx >= clips.Length)
            currentIdx = 0;
    }

}
#endif