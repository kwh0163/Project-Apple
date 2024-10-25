using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableObject : MonoBehaviour
{
    private Outline outline;
    public Outline OutLine => outline;
    [SerializeField] private bool isStatic;
    public bool IsStatic => isStatic;
    [SerializeField] private bool isFlipped;
    public bool IsFlipped => isFlipped;
    [SerializeField] private ObjectType objectType;
    public ObjectType Type => objectType;
    public Collider Collider { get; private set; }
    public bool IsOverlapped { get; private set; }

    protected Vector3 DefaultPosition { get; private set; }
    protected Quaternion DefaultRotation { get; private set; }

    private readonly List<GameObject> overlapBlocks = new();

    public virtual void Initialize()
    {
        DefaultPosition = transform.position;
        DefaultRotation = transform.rotation;

        outline = GetComponent<Outline>();

        Collider = GetComponent<Collider>();

        Release();
    }
    public void Select(Color color)
    {
        outline.OutlineColor = color;
        outline.enabled = true;
    }
    public void Release()
    {
        outline.enabled = false;
    }
    public virtual void DestoryObject()
    {

    }
    public virtual void FlipBlock()
    {
        isFlipped = !isFlipped;
        transform.Rotate(Vector3.up, 180, Space.World);
    }
    public virtual void MovePosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public abstract void PlayStage();
    public virtual void ResetStage()
    {
        Release();
        transform.SetPositionAndRotation(DefaultPosition, DefaultRotation);
    }

    public bool CheckOverlapped(GameObject ignoreObject)
    {
        for (int i = 0; i < overlapBlocks.Count; i++)
        {
            if (ignoreObject != null && overlapBlocks[i] == ignoreObject)
                continue;
            else
                return true;
        }
        return false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.Stage.CurrentState == StageState.Prepare)
            if (!overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Add(collision.gameObject);
    }
    protected virtual void OnCollisionExit(Collision collision)
    {
        if (GameManager.Instance.Stage.CurrentState == StageState.Prepare)
            if (overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Remove(collision.gameObject);
    }

}
