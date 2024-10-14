using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableObject : MonoBehaviour
{
    [SerializeField] private bool isStatic;
    public bool IsStatic { get { return isStatic; } }
    [SerializeField] private bool isFlipped;
    public bool IsFlipped { get { return isFlipped; } }
    [SerializeField] private ObjectType objectType;
    public ObjectType Type { get { return objectType; } }
    public Collider Collider { get; private set; }
    public bool IsOverlapped { get; private set; }

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private List<GameObject> overlapBlocks = new();

    public virtual void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        Collider = GetComponent<Collider>();
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
        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
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
