using UnityEngine;

public class Block : MonoBehaviour
{
    private Collider collider;

    public bool IsOverlapped { get; private set; }

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        collider = GetComponent<Collider>();
    }

    public void MovePosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public bool CheckIsContained(Collider targetCollider)
    {
        Bounds targetBounds = targetCollider.bounds;
        Bounds objectBounds = collider.bounds;

        return (targetBounds.Contains(objectBounds.min) && targetBounds.Contains(objectBounds.max));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            IsOverlapped = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            IsOverlapped = false;
        }
    }
}
