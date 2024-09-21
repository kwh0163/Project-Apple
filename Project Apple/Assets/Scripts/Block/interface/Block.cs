using UnityEngine;

public class Block : MonoBehaviour
{
    public bool IsOverlapped { get; private set; }
    public void MovePosition(Vector3 pos)
    {
        transform.position = pos;
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
