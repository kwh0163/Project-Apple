#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<GameObject> list;
    private void Start()
    {
        list = new List<GameObject>();
        foreach(var ele in FindObjectsOfType<GameObject>())
        {
            list.Add(ele);
        }
        StartCoroutine(Iasdf());
    }

    IEnumerator Iasdf()
    {
        yield return new WaitForSeconds(3);
        list.Clear();
        yield return null;
    }
}
#endif