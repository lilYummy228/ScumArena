using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Cell _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _offset = 0.1f;    

    public event Action<IReadOnlyList<Cell>> GridGenerated;

    private Vector3 CellSize => _prefab.GetComponent<MeshRenderer>().bounds.size;

    public void GenerateGrid()
    {
        List<Cell> cellInGrid = new List<Cell>();

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 position = new Vector3(x * (CellSize.x + _offset), 0, y * (CellSize.z + _offset));

                Cell cell = Instantiate(_prefab, position, Quaternion.identity, _parent);

                cell.SetCoordinates(x, y);

                cellInGrid.Add(cell);

                cell.name = $"X: {x}, Y: {y}";
            }
        }

        GridGenerated?.Invoke(cellInGrid);

        Debug.Log("Grid Generated");
    }
}
