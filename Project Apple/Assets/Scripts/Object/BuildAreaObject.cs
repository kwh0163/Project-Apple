using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAreaObject : MonoBehaviour
{
    public BoxCollider Collider { get; private set; }

    public void Initialize()
    {
        Collider = GetComponent<BoxCollider>();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
