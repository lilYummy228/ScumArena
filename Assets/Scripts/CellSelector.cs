using System.Collections;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    public Cell CurrentCell { get; private set; }

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public void StartSelection() =>
        StartCoroutine(Select());

    private IEnumerator Select()
    {
        while (enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
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

            yield return _waitForFixedUpdate;
        }
    }
}
