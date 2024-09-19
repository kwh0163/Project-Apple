using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleObject : MonoBehaviour
{
    [SerializeField] private float gravity;
    private Vector3 force;
    private Rigidbody rb;

    void Start()
    {
        force = new Vector3(0, gravity, 0);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 사용자 정의 중력을 적용
        rb.AddForce(force, ForceMode.Acceleration);
    }
}
