using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Cell _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _offset = 0.1f;

    private Vector3 CellSize => _prefab.GetComponent<MeshRenderer>().bounds.size;

    [ContextMenu("Generate grid")]
    private void GenerateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 position = new Vector3(x * (CellSize.x + _offset), 0, y * (CellSize.z + _offset));

                Cell cell = Instantiate(_prefab, position, Quaternion.identity, _parent);

                cell.name = $"X: {x}, Y: {y}";
            }
        }
    }
}
