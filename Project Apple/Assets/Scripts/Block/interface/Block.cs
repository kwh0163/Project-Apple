using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private bool isStatic;
    public bool IsStatic { get { return isStatic; } }

    [SerializeField] private bool isFlipped;
    public Collider Collider { get; private set; }

    [SerializeField] private BlockType blockType;
    public BlockType Type { get { return blockType; } }

    public Rigidbody Rigid { get; private set; }

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    public bool IsOverlapped { get; private set; }
    public bool IsFlipped { get { return isFlipped; } }

    private List<GameObject> overlapBlocks = new();

    public virtual void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        Collider = GetComponent<Collider>();
        Rigid = GetComponent<Rigidbody>();
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

    public virtual void ResetStage()
    {
        transform.SetPositionAndRotation(defaultPosition, defaultRotation);
        Rigid.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.Stage.CurrentState == StageState.Prepare)
            if (!overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Add(collision.gameObject);
        if (collision.collider.CompareTag("Apple"))
        {
            if (GameManager.Instance.Stage.CurrentState == StageState.Play)
                OnAppleEnter(collision);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (GameManager.Instance.Stage.CurrentState == StageState.Prepare)
            if (overlapBlocks.Contains(collision.gameObject))
                overlapBlocks.Remove(collision.gameObject);
        if (collision.collider.CompareTag("Apple"))
        {
            if (GameManager.Instance.Stage.CurrentState == StageState.Play)
                OnAppleExit(collision);
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
    protected virtual void OnDestroy()
    {
        
    }

    protected virtual void OnAppleEnter(Collision collision)
    {
        collision.collider.GetComponent<AppleObject>().PrevBlock = this;
    }
    protected virtual void OnAppleExit(Collision collision)
    {

    }
}
