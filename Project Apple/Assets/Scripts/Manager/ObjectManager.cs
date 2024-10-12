using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<AppleObject> AppleList { get; private set; }
    public NewtonObject Newton { get; private set; }
    public List<Block> BlockList { get; private set; }
    public List<BuildAreaObject> AreaList { get; private set; }
    public List<Block> PlacedBlockList { get; private set; }

    public void Initialize()
    {
        AppleList = new List<AppleObject>();
        BlockList = new List<Block>();
        AreaList = new List<BuildAreaObject>();
        PlacedBlockList = new List<Block>();
    }

    public void SetStage(GameObject root)
    {
        AppleList.Clear();
        BlockList.Clear();
        AreaList.Clear();
        PlacedBlockList.Clear();


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
        var areas = root.GetComponentsInChildren<BuildAreaObject>();
        foreach (var ele in areas)
        {
            AreaList.Add(ele);
            ele.Initialize();
        }

        Newton.Initialize();
    }

    public void ResetObject()
    {
        Newton.ResetStage();
        foreach (var ele in AreaList)
            ele.SetActive(true);
        foreach (var ele in AppleList)
            ele.ResetStage();
        foreach (var ele in BlockList)
            ele.ResetStage();
    }
    public void PlayApple()
    {
        foreach (var ele in AreaList)
            ele.SetActive(false);
        foreach (var ele in BlockList)
            ele.Rigid.isKinematic = true;
        foreach (var ele in PlacedBlockList)
            ele.Rigid.isKinematic = true;
        foreach (var ele in AppleList)
            ele.PlayApple();
    }

    public bool CheckIsContained(Collider targetCollider)
    {
        Bounds targetBounds = targetCollider.bounds;

        for(int i = 0; i < AreaList.Count; i++)
        {
            Bounds areaBounds = AreaList[i].Collider.bounds;
            if (areaBounds.Contains(targetBounds.min) && areaBounds.Contains(targetBounds.max))
                return true;
        }
        return false;
    }
}
