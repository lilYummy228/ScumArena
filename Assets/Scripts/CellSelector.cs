using System.Collections;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    private Coroutine _selectionCoroutine;
    private WaitUntil _waitUntil = new WaitUntil(() => Input.GetMouseButtonDown(0));

    public Cell CurrentCell { get; private set; }

    public void StartSelection() =>
       _selectionCoroutine = StartCoroutine(Select());

    public void StopSelection()
    {
        if (_selectionCoroutine != null)
            StopCoroutine(_selectionCoroutine);
    }

    public void ClearCellSelection() =>
        CurrentCell = null;

    private IEnumerator Select()
    {
        while (enabled)
        {
            yield return _waitUntil;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {
                    if (CurrentCell == null)
                    {
                        cell.ChangeMaterial();
                        CurrentCell = cell;
                    }
                    else
                    {
                        CurrentCell.ChangeMaterial();
                        CurrentCell = cell;
                        CurrentCell.ChangeMaterial();
                    }
                }
            }
        }
    }
}