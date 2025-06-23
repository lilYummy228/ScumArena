using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{   
    [SerializeField] private Vector2Int _coordinates;

    private MeshRenderer _meshRenderer;
    private Material _currentMaterial;

    public Vector2Int Coordinates => _coordinates;

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    public void SetCoordinates(int x, int y) =>
        _coordinates = new Vector2Int(x, y);

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
        _currentMaterial = material;
    }
}
