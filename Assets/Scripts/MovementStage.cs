using System;
using System.Collections;
using UnityEngine;

public class MovementStage : MonoBehaviour
{
    [SerializeField] private float _speed = 2;    

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public event Action MovementStageFinished;

    public void MoveTo(Unit player, Cell cell)
    {
        Debug.Log("Movement Stage Started");

        StartCoroutine(SmoothlyMove(player, cell));
    }

    public IEnumerator SmoothlyMove(Unit player, Cell cell)
    {
        Vector3 targetPosition = new Vector3(cell.transform.position.x, player.transform.position.y, cell.transform.position.z);

        while (player.transform.position != targetPosition)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, _speed * Time.deltaTime);
            yield return _waitForFixedUpdate;
        }

        player.SetCoordinates(cell.Coordinates.x, cell.Coordinates.y);

        MovementStageFinished?.Invoke();

        Debug.Log("Movement Stage Finished");
    }
}
