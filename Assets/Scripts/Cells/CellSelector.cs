using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _rangeMaterial;

    private readonly Dictionary<Unit, Cell> _unitsChosenCell = new Dictionary<Unit, Cell>();
    private Cell _playerStartCell;
    private Coroutine _selectionCoroutine;
    private WaitUntil _waitUntil = new WaitUntil(() => Input.GetMouseButtonDown(0));

    public IReadOnlyDictionary<Unit, Cell> UnitsChosenCell => _unitsChosenCell;

    public Cell CurrentCell { get; private set; }

    public void StartSelection(IReadOnlyDictionary<Unit, List<Cell>> unitRangeCells, Cell playerStartCell, Unit playerUnit)
    {
        _unitsChosenCell.Clear();

        _playerStartCell = playerStartCell;

        foreach (Cell rangeCell in unitRangeCells[playerUnit])
            rangeCell.SetMaterial(_rangeMaterial);

        _selectionCoroutine = StartCoroutine(PlayerSelect(unitRangeCells[playerUnit]));
    }

    public void StopSelection(IReadOnlyDictionary<Unit, List<Cell>> unitRangeCells, Unit playerUnit)
    {
        if (CurrentCell == null)
            CurrentCell = _playerStartCell;

        foreach (Cell rangeCell in unitRangeCells[playerUnit])
            rangeCell.SetMaterial(_defaultMaterial);

        if (_selectionCoroutine != null)
            StopCoroutine(_selectionCoroutine);

        _unitsChosenCell.Add(playerUnit, CurrentCell);

        EnemiesSelect(unitRangeCells, playerUnit);
    }

    public void ClearCellSelection() =>
        CurrentCell = null;

    private IEnumerator PlayerSelect(IReadOnlyList<Cell> rangeCells)
    {
        while (enabled)
        {
            yield return _waitUntil;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {
                    foreach (Cell rangeCell in rangeCells)
                    {
                        if (rangeCell == cell)
                        {
                            if (CurrentCell == null)
                            {
                                cell.SetMaterial(_selectedMaterial);

                                CurrentCell = cell;
                            }
                            else
                            {
                                CurrentCell.SetMaterial(_rangeMaterial);

                                CurrentCell = cell;

                                CurrentCell.SetMaterial(_selectedMaterial);
                            }
                        }
                    }
                }
            }
        }
    }

    private void EnemiesSelect(IReadOnlyDictionary<Unit, List<Cell>> unitRangeCells, Unit playerUnit)
    {
        foreach (Unit unit in unitRangeCells.Keys)
        {
            if (unit == playerUnit)
                continue;

            int randomCellIndex = Random.Range(0, unitRangeCells[unit].Count);

            _unitsChosenCell.Add(unit, unitRangeCells[unit][randomCellIndex]);
        }
    }
}