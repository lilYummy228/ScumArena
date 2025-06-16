using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2Int[] _spawnPoints;
    [SerializeField] private Transform _parent;

    public event Action<Unit> UnitSpawned;

    public void SpawnUnit(IReadOnlyList<Cell> grid, Unit prefab)
    {
        Vector2Int randomPoint = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length - 1)];

        foreach (Cell cell in grid)
        {
            if (cell.Coordinates == randomPoint)
            {
                Unit unit = Instantiate(prefab, new Vector3(cell.transform.position.x, prefab.transform.position.y, cell.transform.position.z), Quaternion.identity, _parent);
                unit.SetCoordinates(randomPoint.x, randomPoint.y);

                UnitSpawned?.Invoke(unit);

                Debug.Log("Unit Spawned");

                break;
            }
        }  
    }
}
