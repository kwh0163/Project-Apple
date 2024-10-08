using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private bool isStatic;
    public bool IsStatic { get { return isStatic; } }
    [SerializeField] private bool isFlipped;
    private Collider colliderComponent;
    [SerializeField] private BlockType blockType;
    public BlockType Type { get { return blockType; } }

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    public bool IsOverlapped { get; private set; }
    public bool IsFlipped { get { return isFlipped; } }
    protected UnityEvent<Collision> onAppleEnterEvent = new();
    protected UnityEvent<Collision> onAppleExitEvent = new();

    private List<GameObject> overlapBlocks = new();

    public virtual void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        colliderComponent = GetComponent<Collider>();
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

    public bool CheckIsContained(Collider targetCollider)
    {
        Bounds targetBounds = targetCollider.bounds;
        Bounds objectBounds = colliderComponent.bounds;

        return (targetBounds.Contains(objectBounds.min) && targetBounds.Contains(objectBounds.max));
    }

    public virtual void ResetStage()
    {
        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Block"))
        {
            if (!overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Add(collision.gameObject);
        }
        if (collision.collider.CompareTag("Apple"))
        {
            onAppleEnterEvent.Invoke(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if (overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Remove(collision.gameObject);
        }
        if (collision.collider.CompareTag("Apple"))
        {
            onAppleExitEvent.Invoke(collision);
        }
    }
    public bool CheckOverlapped(GameObject ignoreObject)
    {
        for(int i = 0; i < overlapBlocks.Count; i++)
        {
            if (ignoreObject != null && overlapBlocks[i] == ignoreObject)
                continue;
            else
                return true;
        }
        return false;
    }
}
