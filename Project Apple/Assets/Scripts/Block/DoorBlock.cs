using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBlock : InteractableBlock
{
    [Space()]
    [SerializeField] float openingTime;
    [SerializeField] Vector3 targetPosition;
    float timeCounter;

    Vector3 worldTargetPosition;

    Coroutine currentCoroutine;

    bool isInteracted;

    public override void Initialize()
    {
        base.Initialize();
        timeCounter = 0;
        isInteracted = false;
    }
    public override void ResetStage()
    {
        base.ResetStage();
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        isInteracted = false;
        timeCounter = 0;

    }
    public override void Interact()
    {
        if (isInteracted)
            return;
        currentCoroutine = StartCoroutine(OpenCoroutine());
    }

    IEnumerator OpenCoroutine()
    {
        isInteracted = true;
        while (timeCounter < openingTime)
        {
            timeCounter += Time.deltaTime;
            float a = timeCounter / openingTime;
            Vector3 newPosition = Vector3.Lerp(DefaultPosition, DefaultPosition + (IsFlipped ? -targetPosition : targetPosition), a);

            MovePosition(newPosition);
            yield return null;
        }
        yield return null;
    }
}
