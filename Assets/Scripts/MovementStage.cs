using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStage : MonoBehaviour
{
    [SerializeField] private float _speed = 2;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public event Action MovementStageFinished;

    public void MoveTo(IReadOnlyDictionary<Unit, Cell> unitsCell)
    {
        Debug.Log("Movement Stage Started");

        StartCoroutine(SmoothlyMove(unitsCell));
    }

    public IEnumerator SmoothlyMove(IReadOnlyDictionary<Unit, Cell> unitsCell)
    {
        foreach (KeyValuePair<Unit, Cell> unitCellPair in unitsCell)
        {
            Vector3 targetPosition = new Vector3(unitCellPair.Value.transform.position.x, unitCellPair.Key.transform.position.y, unitCellPair.Value.transform.position.z);

            while (unitCellPair.Key.transform.position != targetPosition)
            {
                unitCellPair.Key.transform.position = Vector3.MoveTowards(unitCellPair.Key.transform.position, targetPosition, _speed * Time.deltaTime);
                yield return _waitForFixedUpdate;
            }

            unitCellPair.Key.SetCoordinates(unitCellPair.Value.Coordinates.x, unitCellPair.Value.Coordinates.y);
        }

        MovementStageFinished?.Invoke();

        Debug.Log("Movement Stage Finished");
    }
}
