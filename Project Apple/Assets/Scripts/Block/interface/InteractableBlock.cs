using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBlock : Block, InteractInterface
{
    [SerializeField] private ConnectType connectType;
    public ConnectType ConnectType => connectType;
    [SerializeField] bool isConnected;
    public bool IsConnected => isConnected;
    public abstract void Interact();
}
