using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStage : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private float _timeToMove = 4f;

    public event Action MovementStageFinished;

    private WaitForSeconds WaitForMoving => new WaitForSeconds(_timeToMove);

    public void MoveTo(IReadOnlyDictionary<Unit, Cell> unitsCell)
    {
        Debug.Log("Movement Stage Started");

        bool[] isMoved = new bool[unitsCell.Count];

        foreach (KeyValuePair<Unit, Cell> unitCellPair in unitsCell)
            StartCoroutine(SmoothlyMove(unitCellPair));

        StartCoroutine(AwaitMovement());
    }

    private IEnumerator AwaitMovement()
    {
        yield return WaitForMoving;

        MovementStageFinished?.Invoke();

        Debug.Log("Movement Stage Finished");
    }

    private IEnumerator SmoothlyMove(KeyValuePair<Unit, Cell> unitCellPair)
    {
        Vector3 targetPosition = new Vector3(unitCellPair.Value.transform.position.x, unitCellPair.Key.transform.position.y, unitCellPair.Value.transform.position.z);

        while (unitCellPair.Key.transform.position != targetPosition)
        {
            unitCellPair.Key.transform.position = Vector3.MoveTowards(unitCellPair.Key.transform.position, targetPosition, _speed * Time.deltaTime);
            yield return _waitForFixedUpdate;
        }

        unitCellPair.Key.SetCoordinates(unitCellPair.Value.Coordinates.x, unitCellPair.Value.Coordinates.y);
    }
}
