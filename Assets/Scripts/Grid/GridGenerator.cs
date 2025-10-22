using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Grid _grid;
    [SerializeField] private Cell _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _offset = 0.1f;

    private Vector3 CellSize => _prefab.GetComponent<MeshRenderer>().bounds.size;

    [ContextMenu("Generate grid")]
    public void GenerateGrid()
    {
        List<Cell> cells = new List<Cell>();

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 position = new Vector3(x * (CellSize.x + _offset), 0, y * (CellSize.z + _offset));

                Cell cell = Instantiate(_prefab, position, Quaternion.identity, _parent);

                cell.SetCoordinates(x, y);

                cell.name = $"X: {x}, Y: {y}";

                cells.Add(cell);
            }
        }

        _grid.SetGrid(cells, _gridSize);

        Debug.Log("Grid Generated");
    }
}
