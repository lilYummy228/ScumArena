using System;
using System.Collections;
using UnityEngine;

public class PreparationStage : MonoBehaviour
{
    [SerializeField] private float _preparationTime;
    [SerializeField] private CellSelector _cellSelector;

    private WaitForSeconds _wait;

    public event Action<Cell> PreparationStageFinished;

    public void Prepare()
    {
        _wait = new WaitForSeconds(_preparationTime);

        StartCoroutine(CountPreparationTime());

        _cellSelector.StartSelection();

        Debug.Log("Praparation Stage Started");
    }

    private IEnumerator CountPreparationTime()
    {
        yield return _wait;

        _cellSelector.StopSelection();

        PreparationStageFinished?.Invoke(_cellSelector.CurrentCell);

        _cellSelector.ClearCellSelection();

        Debug.Log("Praparation Stage Finished");
    }
}
