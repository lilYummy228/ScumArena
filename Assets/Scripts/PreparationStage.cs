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
        Cell startCell = null;

        _wait = new WaitForSeconds(_preparationTime);

        foreach (Cell cell in grid)
        {
            if (Mathf.Abs(cell.Coordinates.x - player.Coordinate.x) <= player.MovementRange &&
                Mathf.Abs(cell.Coordinates.y - player.Coordinate.y) <= player.MovementRange)
            {
                rangeCells.Add(cell);
            }

            if (cell.Coordinates.x == player.Coordinate.x &&
                cell.Coordinates.y == player.Coordinate.y)
            {
                startCell = cell;
            }
        }

        StartCoroutine(CountPreparationTime(rangeCells));
        _cellSelector.StartSelection(rangeCells, startCell);

        Debug.Log("Preparation Stage Started");
    }

    private IEnumerator CountPreparationTime(IReadOnlyList<Cell> rangeCells)
    {
        yield return _wait;

        _cellSelector.StopSelection(rangeCells);

        PreparationStageFinished?.Invoke(_cellSelector.CurrentCell);

        _cellSelector.ClearCellSelection();

        Debug.Log("Preparation Stage Finished");
    }
}
