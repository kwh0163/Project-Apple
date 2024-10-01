using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBlock : Block
{
    [SerializeField] private float power;
    private Vector3 accelDirection = Vector3.right;
    public override void Initialize()
    {
        base.Initialize();

        SetAccelDirection();

        onAppleEnterEvent.AddListener(AddForce);
    }
    private void AddForce(Collision collision)
    {
        collision.collider.GetComponent<AppleObject>().AddForce(accelDirection.normalized * power);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + accelDirection * 4);
    }

    public override void ResetStage()
    {
        base.ResetStage();
        SetAccelDirection();
    }
    public override void MovePosition(Vector3 pos)
    {
        base.MovePosition(pos);
        SetAccelDirection();
    }
    public override void ChangeRotation(Quaternion quaternion)
    {
        base.ChangeRotation(quaternion);
        SetAccelDirection();
    }

    private void SetAccelDirection() 
    {
        accelDirection = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * Vector3.right;
        SetLineRenderer();
    }

    public LineRenderer line;
    private void SetLineRenderer()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + accelDirection.normalized * 3);
    }

}
