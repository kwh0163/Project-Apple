using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<AppleObject> AppleList { get; private set; }
    public NewtonObject Newton { get; private set; }
    public List<Block> BlockList { get; private set; }
    public List<Block> SpareBlock { get; private set; }

    public void Initialize(GameObject root)
    {
        if (AppleList == null)
            AppleList = new List<AppleObject>();
        else
            AppleList.Clear();
        if (BlockList == null)
            BlockList = new List<Block>();
        else
            BlockList.Clear();
        if (SpareBlock == null)
            SpareBlock = new List<Block>();
        else
            SpareBlock.Clear();

        Newton = root.GetComponentInChildren<NewtonObject>();
        var apples = root.GetComponentsInChildren<AppleObject>();
        foreach (var ele in apples)
        {
            AppleList.Add(ele);
            ele.Initialize();
        }
        var blocks = root.GetComponentsInChildren<Block>();
        foreach (var ele in blocks)
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
        foreach (var ele in SpareBlock)
            Destroy(ele.gameObject);
        SpareBlock.Clear();
    }
    public void PlayApple()
    {
        foreach (var ele in AppleList)
            ele.PlayApple();
    }
}
