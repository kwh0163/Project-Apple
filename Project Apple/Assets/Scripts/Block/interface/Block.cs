using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private bool isFlipped;
    private Collider colliderComponent;

    Vector3 defaultPosition;
    Quaternion defaultRotation;

    public bool IsOverlapped { get; private set; }
    public bool IsFlipped { get { return isFlipped; } }
    protected UnityEvent<Collision> onAppleEnterEvent = new();
    protected UnityEvent<Collision> onAppleExitEvent = new();

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
