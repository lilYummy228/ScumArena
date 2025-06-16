using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _rangeMaterial;

    private Coroutine _selectionCoroutine;
    private WaitUntil _waitUntil = new WaitUntil(() => Input.GetMouseButtonDown(0));

    public Cell CurrentCell { get; private set; }

    public void StartSelection(IReadOnlyList<Cell> cells)
    {
        foreach (Cell rangeCell in cells)
            rangeCell.SetMaterial(_rangeMaterial);

        _selectionCoroutine = StartCoroutine(Select(cells));
    }

    public void StopSelection(IReadOnlyList<Cell> rangeCells)
    {
        foreach (Cell rangeCell in rangeCells)
            rangeCell.SetMaterial(_defaultMaterial);

        if (_selectionCoroutine != null)
            StopCoroutine(_selectionCoroutine);
    }

    public void ClearCellSelection() =>
        CurrentCell = null;

    private IEnumerator Select(IReadOnlyList<Cell> rangeCells)
    {
        while (enabled)
        {
            yield return _waitUntil;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {
                    foreach (Cell rangeCell in rangeCells)
                    {                       
                        if (rangeCell == cell)
                        {
                            if (CurrentCell == null)
                            {
                                cell.SetMaterial(_selectedMaterial);

                                CurrentCell = cell;
                            }
                            else
                            {
                                CurrentCell.SetMaterial(_rangeMaterial);

                                CurrentCell = cell;

                                CurrentCell.SetMaterial(_selectedMaterial);
                            }
                        }
                    }
                }
            }
        }
    }
}