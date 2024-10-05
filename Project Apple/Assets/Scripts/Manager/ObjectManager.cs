using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<AppleObject> AppleList { get; private set; }
    public NewtonObject Newton { get; private set; }
    public List<Block> BlockList { get; private set; }

    public void Initialize()
    {
        Newton = FindObjectOfType<NewtonObject>();
        AppleList = new List<AppleObject>();
        BlockList = new List<Block>();

        foreach(var ele in FindObjectsOfType<AppleObject>())
        {
            AppleList.Add(ele);
            ele.Initialize();
        }
        foreach (var ele in FindObjectsOfType<Block>())
        {
            BlockList.Add(ele);
            ele.Initialize();
        }

        Newton.Initialize();
    }

    public void ResetObject()
    {
        Newton.ResetStage();
        foreach (var ele in AppleList)
            ele.ResetStage();
        foreach (var ele in BlockList)
            ele.ResetStage();
    }

    public void PlayApple()
    {
        foreach (var ele in AppleList)
            ele.PlayApple();
    }
}
