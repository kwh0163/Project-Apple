using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private NewtonObject newton;
    private List<BuildAreaObject> areaList;
    private List<MovableObject> placedObjectList;
    private List<InteractableBlock> interactList;
    
    public void Initialize()
    {
        areaList = new();
        placedObjectList = new();
        interactList = new();
    }

    public void SetStage(GameObject root)
    {
        areaList.Clear();
        placedObjectList.Clear();


        newton = root.GetComponentInChildren<NewtonObject>();
        var areas = root.GetComponentsInChildren<BuildAreaObject>();
        foreach (var ele in areas)
        {
            areaList.Add(ele);
            ele.Initialize();
        }
        var objects = root.GetComponentsInChildren<MovableObject>();
        foreach(var ele in objects)
        {
            placedObjectList.Add(ele);
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
        foreach (var ele in placedObjectList)
            ele.ResetStage();
        foreach (var ele in interactList)
            ele.ResetStage();
    }
    public void PlayStage()
    {
        foreach (var ele in areaList)
            ele.SetActive(false);
        foreach (var ele in placedObjectList)
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
        if (IsInteract(movableObject, out InteractableBlock temp))
            interactList.Add(temp);
        else
            placedObjectList.Add(movableObject);
    }
    public void RemoveMovableObject(MovableObject movableObject)
    {
        if (IsInteract(movableObject, out InteractableBlock temp))
            interactList.Remove(temp);
        else
            placedObjectList.Remove(movableObject);
    }
    bool IsInteract(MovableObject movable, out InteractableBlock interact)
    {
        if(movable is InteractableBlock)
        {
            interact = (InteractableBlock)movable;
            return true;
        }
        interact = null;
        return false;
    }
}
