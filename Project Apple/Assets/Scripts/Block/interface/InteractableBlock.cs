using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBlock : Block
{
    [SerializeField] private ConnectType connectType;
    [SerializeField] private Vector3 connectPosition;
    public Vector3 ConnectPosition 
    { 
        get
        {
            return DefaultPosition + (IsFlipped ? -connectPosition : connectPosition);
        } 
    }

    public ConnectType ConnectType => connectType;
    private bool isConnected;
    public bool IsConnected => isConnected;
    private TriggerBlock connectedTrigger;
    public abstract void Interact();
    public override void Initialize()
    {
        base.Initialize();
        if (connectedTrigger != null)
            connectedTrigger.SetLinePosition(ConnectPosition);
    }
    public void Connect(TriggerBlock triggerBlock)
    {
        if (connectedTrigger != null && (isConnected && triggerBlock != connectedTrigger))
            connectedTrigger.Disconnect();
        connectedTrigger = triggerBlock;
        isConnected = true;
    }
    public void Disconnect()
    {
        isConnected = false;
        connectedTrigger = null;
    }
    public override void DestoryObject()
    {
        base.DestoryObject();
        if (IsConnected)
            connectedTrigger.Disconnect();
    }
}
