using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationStage : MonoBehaviour
{
    [SerializeField] private float _preparationTime;
    [SerializeField] private CellSelector _cellSelector;

    private WaitForSeconds _wait;

    public event Action<Cell> PreparationStageFinished;

    public void Prepare(Unit player, IReadOnlyList<Cell> grid)
    {
        List<Cell> rangeCells = new List<Cell>();
        _wait = new WaitForSeconds(_preparationTime);

        foreach (Cell cell in grid)
            if (Mathf.Abs(cell.Coordinates.x  - player.Coordinate.x) <= player.UnitMover.Range &&
                Mathf.Abs(cell.Coordinates.y - player.Coordinate.y) <= player.UnitMover.Range)
                rangeCells.Add(cell);

        StartCoroutine(CountPreparationTime(rangeCells));
        _cellSelector.StartSelection(rangeCells);

        Debug.Log("Praparation Stage Started");
    }

    private IEnumerator CountPreparationTime(IReadOnlyList<Cell> rangeCells)
    {
        yield return _wait;

        _cellSelector.StopSelection(rangeCells);

        PreparationStageFinished?.Invoke(_cellSelector.CurrentCell);

        _cellSelector.ClearCellSelection();

        Debug.Log("Praparation Stage Finished");
    }
}
