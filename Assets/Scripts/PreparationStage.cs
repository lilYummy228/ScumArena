using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationStage : MonoBehaviour
{
    [SerializeField] private float _preparationTime;
    [SerializeField] private CellSelector _cellSelector;

    private WaitForSeconds _wait;

    public event Action<IReadOnlyDictionary<Unit, Cell>> PreparationStageFinished;

    public void Prepare(IReadOnlyList<Unit> units, IReadOnlyList<Cell> grid)
    {
        Dictionary<Unit, List<Cell>> unitRangeCells = new Dictionary<Unit, List<Cell>>();
        List<Cell> rangeCells = new List<Cell>();
        Cell playerStartCell = null;
        Unit playerUnit = null;

        _wait = new WaitForSeconds(_preparationTime);

        for (int i = 0; i < units.Count; i++)
        {
            rangeCells.Clear();

            foreach (Cell cell in grid)
            {
                //if (Mathf.Abs(cell.Coordinates.x - units[i].Coordinate.x) <= units[i].MovementRange &&
                //    Mathf.Abs(cell.Coordinates.y - units[i].Coordinate.y) <= units[i].MovementRange)
                if (Mathf.Abs((units[i].Coordinate - cell.Coordinates).x) <= units[i].MovementRange && Mathf.Abs((units[i].Coordinate - cell.Coordinates).y) <= units[i].MovementRange)
                    rangeCells.Add(cell);

                if (i == 0)
                    if (cell.Coordinates.x == units[i].Coordinate.x && cell.Coordinates.y == units[i].Coordinate.y)
                        playerStartCell = cell;
            }
            
            if (i == 0)
                playerUnit = units[i];

            unitRangeCells.Add(units[i], rangeCells);
        }

        _cellSelector.StartSelection(unitRangeCells, playerStartCell, playerUnit);
        StartCoroutine(CountPreparationTime(unitRangeCells, playerUnit));

        Debug.Log("Preparation Stage Started");
    }

    private IEnumerator CountPreparationTime(IReadOnlyDictionary<Unit, List<Cell>> rangeCells, Unit playerUnit)
    {
        yield return _wait;

        _cellSelector.StopSelection(rangeCells, playerUnit);

        PreparationStageFinished?.Invoke(_cellSelector.UnitsChosenCell);

        _cellSelector.ClearCellSelection();

        Debug.Log("Preparation Stage Finished");
    }
}
