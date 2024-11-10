#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
        Vector2 temp = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 newPos = Camera.main.transform.position;
        newPos.z *= 1 - (temp.x < temp.y ? temp.x : temp.y);
        Camera.main.transform.position = newPos;
        Debug.Log(Camera.main.WorldToViewportPoint(transform.position));
    }

}
#endif