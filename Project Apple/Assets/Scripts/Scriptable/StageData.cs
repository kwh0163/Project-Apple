using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockData", menuName ="Scriptable/BlockData",order = 0)]
public class StageData : ScriptableObject
{
    [SerializeField] private GameObject stagePrefab;
    [SerializeField] private List<BlockType> blocks;
    public bool isStageUnlocked;
    public bool isStageCleared;
    public GameObject StagePrefab { get { return stagePrefab; } }
    public List<BlockType> BlockData { get { return blocks; } }

}
