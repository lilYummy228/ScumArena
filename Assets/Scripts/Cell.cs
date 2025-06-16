using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{   
    private MeshRenderer _meshRenderer;
    private Material _currentMaterial;

    public Vector2Int Coordinates { get; private set; }

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    public void SetCoordinates(int x, int y) =>
        Coordinates = new Vector2Int(x, y);

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
        _currentMaterial = material;
    }
}
