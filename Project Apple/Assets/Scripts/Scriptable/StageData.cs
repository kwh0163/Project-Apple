using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageData", menuName ="Scriptable/StageData",order = 0)]
public class StageData : ScriptableObject
{
    [SerializeField] private GameObject stagePrefab;
    [SerializeField] private List<ObjectType> blocks;
    public bool isStageUnlocked;
    public bool isStageCleared;
    public GameObject StagePrefab { get { return stagePrefab; } }
    public List<ObjectType> BlockData { get { return blocks; } }

}
