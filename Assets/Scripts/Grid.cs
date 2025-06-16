using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridGenerator _gridGenerator;

    public GridGenerator GridGenerator => _gridGenerator;
    public IReadOnlyList<Cell> CellInGrid {  get; private set; } = new List<Cell>();

    private void OnEnable() =>
        _gridGenerator.GridGenerated += SetGrid;

    private void OnDisable() =>
        _gridGenerator.GridGenerated -= SetGrid;

    private void SetGrid(IReadOnlyList<Cell> cellInGrid) =>
        CellInGrid = cellInGrid;
}
