using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Cell> _cells = new();

    private Vector2Int _gridSize;

    public IReadOnlyList<Cell> CellsInGrid => _cells;

    public void SetGrid(List<Cell> cells, Vector2Int size)
    {
        _cells.Clear();

        _cells = cells;
        _gridSize = size;
    }
}
