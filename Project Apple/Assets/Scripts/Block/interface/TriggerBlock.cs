using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBlock : Block
{
    [SerializeField] private bool isConnected;
    public bool IsConnected => isConnected;

    InteractInterface currentInteract;
    ConnectType connectType;
    public void Connect(InteractInterface interact)
    {
        if (interact.ConnectType != connectType)
            return;
        currentInteract = interact;
        isConnected = true;
    }
    public void Disconnect()
    {
        currentInteract = null;
        isConnected = false;
    }

    protected override void OnAppleEnter(Collision collision)
    {
        currentInteract.Interact();
    }

    
}
