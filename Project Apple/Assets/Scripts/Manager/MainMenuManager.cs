using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private Coroutine currentCoroutine;
    public void Initialize()
    {


    }

    public void StartMenu()
    {
        currentCoroutine = StartCoroutine(PlayApple());
    }

    IEnumerator PlayApple()
    {
        GameManager.Instance.Stage.CurrentState = StageState.Play;
        GameManager.Instance.Stage.StageObject.ResetObject();
        GameManager.Instance.Stage.StageObject.PlayStage();
        yield return new WaitUntil(() => GameManager.Instance.Stage.CurrentState == StageState.End);
        yield return new WaitForSeconds(5f);
        currentCoroutine = StartCoroutine(PlayApple());
        yield return null;
    }

    public void StopCoroutine()
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }
}
