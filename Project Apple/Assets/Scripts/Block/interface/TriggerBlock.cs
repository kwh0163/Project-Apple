using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBlock : Block
{
    [SerializeField] private bool isConnected;
    public bool IsConnected => isConnected;

    LineRenderer lineRenderer;
    [SerializeField]InteractableBlock currentInteract;
    public InteractableBlock ConnectedInteract => currentInteract;
    [SerializeField] ConnectType connectType;
    public ConnectType ConnectType => connectType;

    public override void Initialize()
    {
        base.Initialize();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, DefaultPosition);
        lineRenderer.SetPosition(1, DefaultPosition);
        if (currentInteract != null)
            Connect(currentInteract);
    }

    public void Connect(InteractableBlock interact)
    {
        if (interact.ConnectType != connectType)
            return;
        interact.Connect(this);
        lineRenderer.SetPosition(1, interact.ConnectPosition);
        currentInteract = interact;
        isConnected = true;
        Release();
    }
    public void Disconnect()
    {
        if (currentInteract != null)
        {
            currentInteract.Disconnect();
        }
        lineRenderer.SetPosition(1, DefaultPosition);
        currentInteract = null;
        isConnected = false;
        Select(Color.red);
    }

    public void SetLinePosition(Vector3 vector3)
    {
        lineRenderer.SetPosition(1, vector3);
    }

    protected override void OnAppleEnter(Collision collision)
    {
        if(isConnected)
            currentInteract.Interact();
    }

    public override void DestoryObject()
    {
        base.DestoryObject();
        Disconnect();
    }
}
