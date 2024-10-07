using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlockData", menuName ="Scriptable/BlockData",order = 0)]
public class StageBlockData : ScriptableObject
{
    [SerializeField] private List<BlockType> blocks;
    public List<BlockType> BlockData { get { return blocks; } }
}
