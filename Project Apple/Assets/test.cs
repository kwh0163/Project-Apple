#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    float timeCounter = 0;
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            timeCounter += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("time = " + timeCounter);
            timeCounter = 0;
        }
    }
}
#endif