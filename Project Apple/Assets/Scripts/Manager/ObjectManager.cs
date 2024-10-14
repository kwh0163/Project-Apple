using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public NewtonObject Newton { get; private set; }
    public List<BuildAreaObject> AreaList { get; private set; }
    public List<MovableObject> PlacedObjectList { get; private set; }

    public void Initialize()
    {
        AreaList = new List<BuildAreaObject>();
        PlacedObjectList = new List<MovableObject>();
    }

    public void SetStage(GameObject root)
    {
        AreaList.Clear();
        PlacedObjectList.Clear();


        Newton = root.GetComponentInChildren<NewtonObject>();
        var areas = root.GetComponentsInChildren<BuildAreaObject>();
        foreach (var ele in areas)
        {
            AreaList.Add(ele);
            ele.Initialize();
        }
        var objects = root.GetComponentsInChildren<MovableObject>();
        foreach(var ele in objects)
        {
            PlacedObjectList.Add(ele);
            ele.Initialize();
        }

        Newton.Initialize();
    }

    public void ResetObject()
    {
        Newton.ResetStage();
        foreach (var ele in AreaList)
            ele.SetActive(true);
        foreach (var ele in PlacedObjectList)
            ele.ResetStage();
    }
    public void PlayStage()
    {
        foreach (var ele in AreaList)
            ele.SetActive(false);
        foreach (var ele in PlacedObjectList)
            ele.PlayStage();
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
