using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStage : MonoBehaviour
{
    private readonly float _movingTime = 2f;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public event Action MovementStageFinished;

    private WaitForSeconds WaitForMoving => new WaitForSeconds(_movingTime);

    public void MoveTo(IReadOnlyDictionary<Unit, Cell> unitsCell)
    {
        Debug.Log("Movement Stage Started");

        foreach (KeyValuePair<Unit, Cell> unitCellPair in unitsCell)
            StartCoroutine(SmoothlyMove(unitCellPair));

        StartCoroutine(AwaitMovementFinish());
    }

    private IEnumerator AwaitMovementFinish()
    {
        yield return WaitForMoving;

        MovementStageFinished?.Invoke();

        Debug.Log("Movement Stage Finished");
    }

    private IEnumerator SmoothlyMove(KeyValuePair<Unit, Cell> unitCellPair)
    {
        Vector3 targetPosition = new Vector3(unitCellPair.Value.transform.position.x, unitCellPair.Key.transform.position.y, unitCellPair.Value.transform.position.z);
        float distance = (targetPosition - unitCellPair.Key.transform.position).magnitude;
        float speed = distance / _movingTime;

        while (unitCellPair.Key.transform.position != targetPosition)
        {
            unitCellPair.Key.transform.position = Vector3.MoveTowards(unitCellPair.Key.transform.position, targetPosition, speed * Time.deltaTime);
            yield return _waitForFixedUpdate;
        }

        unitCellPair.Key.SetCoordinates(unitCellPair.Value.Coordinates.x, unitCellPair.Value.Coordinates.y);
    }
}
