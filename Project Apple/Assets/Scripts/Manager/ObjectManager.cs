using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private NewtonObject newton;
    private List<BuildAreaObject> areaList;
    private List<AppleObject> appleList;
    private List<Block> blockList;
    private List<InteractableBlock> interactList;
    
    public void Initialize()
    {
        areaList = new();
        appleList = new();
        blockList = new();
        interactList = new();
    }

    public void SetStage(GameObject root)
    {
        areaList.Clear();
        appleList.Clear();
        blockList.Clear();
        interactList.Clear();

        newton = root.GetComponentInChildren<NewtonObject>();
        var areas = root.GetComponentsInChildren<BuildAreaObject>();
        foreach (var ele in areas)
        {
            areaList.Add(ele);
            ele.Initialize();
        }
        var apples = root.GetComponentsInChildren<AppleObject>();
        foreach(var ele in apples)
        {
            appleList.Add(ele);
            ele.Initialize();
            ele.SetSkin(GameManager.Instance.Skin.GetCurrentAppleSkin());
        }
        var objects = root.GetComponentsInChildren<Block>();
        foreach(var ele in objects)
        {
            blockList.Add(ele);
            ele.Initialize();
        }
        var interacts = root.GetComponentsInChildren<InteractableBlock>();
        foreach(var ele in interacts)
        {
            interactList.Add(ele);
            ele.Initialize();
        }

        newton.Initialize();
    }

    public void ResetObject()
    {
        newton.ResetStage();
        foreach (var ele in areaList)
            ele.SetActive(true);
        foreach (var ele in appleList)
            ele.ResetStage();
        foreach (var ele in blockList)
            ele.ResetStage();
        foreach (var ele in interactList)
            ele.ResetStage();
    }
    public void PlayStage()
    {
        foreach (var ele in areaList)
            ele.SetActive(false);
        foreach (var ele in appleList)
            ele.PlayStage();
        foreach (var ele in blockList)
            ele.PlayStage();
        foreach (var ele in interactList)
            ele.PlayStage();
    }

    public bool CheckIsContained(Collider targetCollider)
    {
        Bounds targetBounds = targetCollider.bounds;

        bool min = false;
        bool max = false;

        for(int i = 0; i < areaList.Count; i++)
        {
            Bounds areaBounds = areaList[i].Collider.bounds;
            if (areaBounds.Contains(targetBounds.min))
                min = true;
            if (areaBounds.Contains(targetBounds.max))
                max = true;
        }


        return (min && max);
    }
    public void EnableInteract(ConnectType connectType)
    {
        foreach(var ele in interactList)
            if(ele.ConnectType == connectType)
                ele.Select(ele.IsConnected ? Color.red : Color.green);
    }
    public void DisableInteract()
    {
        foreach (var ele in interactList)
            ele.Release();
    }
    public void AddMovableObject(MovableObject movableObject)
    {
        if (movableObject is InteractableBlock)
            interactList.Add((InteractableBlock)movableObject);
        else if (movableObject is AppleObject)
            appleList.Add((AppleObject)movableObject);
        else
            blockList.Add((Block)movableObject);
    }
    public void RemoveMovableObject(MovableObject movableObject)
    {
        if (movableObject is InteractableBlock)
            interactList.Remove((InteractableBlock)movableObject);
        else if (movableObject is AppleObject)
            appleList.Remove((AppleObject)movableObject);
        else
            blockList.Remove((Block)movableObject);
    }
}
