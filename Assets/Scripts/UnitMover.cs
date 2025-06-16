using System;
using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private int _range = 2;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    public event Action Moved;

    public int Range => _range;

    public void MoveTo(Cell cell) =>
        StartCoroutine(SmoothlyMove(cell));

    public IEnumerator SmoothlyMove(Cell cell)
    {
        Vector3 targetPosition = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z);

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            yield return _waitForFixedUpdate;
        }

        Moved?.Invoke();
    }
}
