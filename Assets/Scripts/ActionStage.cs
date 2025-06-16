using System;
using System.Collections;
using UnityEngine;

public class ActionStage : MonoBehaviour
{
    public event Action ActionStageFinished;

    public void StartUseActionAbilities()
    {       
        StartCoroutine(UseActionAbilities());

        Debug.Log("Action Stage Started");
    }

    //TODO Action Stage Logic
    private IEnumerator UseActionAbilities()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("Action Stage Finished");

        ActionStageFinished?.Invoke();
    }
}
