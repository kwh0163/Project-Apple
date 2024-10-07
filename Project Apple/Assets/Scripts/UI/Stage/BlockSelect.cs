using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelect : MonoBehaviour
{
    [SerializeField] private GameObject selectObject;
    [SerializeField] private List<BlockType> blockKey;
    [SerializeField] private List<BlockImage> blockValue;

    private Dictionary<BlockType, BlockImage> blockImages;

    StageBlockData currentData;

    public void Initialize()
    {
        blockImages = new();
        for (int i = 0; i < blockKey.Count; i++)
        {
            blockValue[i].Initialize();
            blockImages.Add(blockKey[i], blockValue[i]);
        }
    }

    public void UseBlock(BlockType block)
    {
        blockImages[block].UseBlock();
    }
    public void RemoveBlock(BlockType block)
    {
        blockImages[block].AddBlock();
    }
    public void SetImages(StageBlockData blockData)
    {
        currentData = blockData;
        foreach (var ele in blockValue)
            ele.ResetImage();
        foreach (var ele in blockData.BlockData)
            blockImages[ele].AddBlock();
    }
    public void ResetImages()
    {
        foreach (var ele in blockValue)
            ele.ResetImage();
        foreach(var ele in currentData.BlockData)
            blockImages[ele].AddBlock();
    }
    public void SetActive(bool isActive)
    {
        selectObject.SetActive(isActive);
    }
}
