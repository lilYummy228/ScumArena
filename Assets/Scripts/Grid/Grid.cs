using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Cell> _cells;

    private Vector2Int _gridSize;

    public IReadOnlyList<Cell> CellsInGrid => _cells;
    public int GridSizeX => _gridSize.x;

    public void SetGrid(List<Cell> cells, Vector2Int size)
    {
        _cells.Clear();

        _cells = cells;
        _gridSize = size;
    }
}
