using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    private Collider collider;

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    public bool IsOverlapped { get; private set; }
    protected UnityEvent<Collision> onAppleEnterEvent = new UnityEvent<Collision>();
    protected UnityEvent<Collision> onAppleExitEvent = new UnityEvent<Collision>();

    public virtual void Initialize()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;

        collider = GetComponent<Collider>();
    }

    public virtual void MovePosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public virtual void ChangeRotation(Quaternion quaternion)
    {
        transform.rotation = quaternion;
    }

    public bool CheckIsContained(Collider targetCollider)
    {
        Bounds targetBounds = targetCollider.bounds;
        Bounds objectBounds = collider.bounds;

        return (targetBounds.Contains(objectBounds.min) && targetBounds.Contains(objectBounds.max));
    }

    public virtual void ResetStage()
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            IsOverlapped = true;
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
            IsOverlapped = false;
        }
        if (collision.collider.CompareTag("Apple"))
        {
            onAppleExitEvent.Invoke(collision);
        }
    }
}
