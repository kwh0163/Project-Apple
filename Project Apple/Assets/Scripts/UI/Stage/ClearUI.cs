using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearUI : MonoBehaviour
{
    [SerializeField] private RectTransform window;
    [SerializeField] private float waitTime;
    [SerializeField] private float openTime;
    Vector3 defaultScale;
    public void Initialize()
    {
        defaultScale = window.localScale;
        Close();
    }
    public void Open()
    {
        window.gameObject.SetActive(true);
        StartCoroutine(OpenCoroutine());
    }
    public void Close()
    {
        window.localScale = Vector3.zero;
        window.gameObject.SetActive(false);
    }

    IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        float timeCounter = 0;
        while(timeCounter <= openTime)
        {
            timeCounter += Time.deltaTime;

            window.localScale = Vector3.Lerp(Vector3.zero, defaultScale, Mathf.Clamp(timeCounter / openTime, 0, 1));

            yield return null;
        }
        yield return null;
    }

}
