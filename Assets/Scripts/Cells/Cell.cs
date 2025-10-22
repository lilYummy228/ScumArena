using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{   
    [SerializeField] private Vector2Int _coordinates;

    private MeshRenderer _meshRenderer;
    private Material _currentMaterial;
    private Ability _ability;

    public Vector2Int Coordinates => _coordinates;
    public Ability Ability => _ability;

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    public void SetCoordinates(int x, int y) =>
        _coordinates = new Vector2Int(x, y);

    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
        _currentMaterial = material;
    }

    public void SetAbility(Ability ability)
    {
        _ability = ability;
    }
}