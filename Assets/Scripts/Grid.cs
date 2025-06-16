using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;

    public event Action GridSet;

    public GridGenerator GridGenerator => _gridGenerator;
    public IReadOnlyList<Cell> CellsInGrid {  get; private set; } = new List<Cell>();

    private void OnEnable() =>
        _gridGenerator.GridGenerated += SetGrid;

    private void OnDisable() =>
        _gridGenerator.GridGenerated -= SetGrid;

    private void SetGrid(IReadOnlyList<Cell> cellsInGrid)
    {
        CellsInGrid = cellsInGrid;
        GridSet?.Invoke();
    }
}
