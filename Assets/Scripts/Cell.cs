using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;

    private MeshRenderer _meshRenderer;
    private Material _currentMaterial;

    private void Start() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    public void ChangeMaterial()
    {
        if (_currentMaterial != _selectedMaterial)
        {
            _meshRenderer.material = _selectedMaterial;
            _currentMaterial = _selectedMaterial;
        }
        else
        {
            _meshRenderer.material = _defaultMaterial;
            _currentMaterial = _defaultMaterial;
        }
    }
}
