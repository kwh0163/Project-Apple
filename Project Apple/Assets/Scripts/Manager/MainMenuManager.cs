using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    public void Initialize()
    {


    }

    public void StartMenu()
    {
        StartCoroutine(PlayApple());
    }

    IEnumerator PlayApple()
    {
        GameManager.Instance.Stage.CurrentState = StageState.Play;
        GameManager.Instance.Stage.StageObject.ResetObject();
        GameManager.Instance.Stage.StageObject.PlayApple();
        yield return new WaitUntil(() => GameManager.Instance.Stage.CurrentState == StageState.End);
        yield return new WaitForSeconds(5f);
        StartCoroutine(PlayApple());
        yield return null;
    }


}
