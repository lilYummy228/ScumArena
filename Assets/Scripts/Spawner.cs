using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2Int[] _spawnPoints;
    [SerializeField] private Transform _parent;

    private List<Unit> _spawnedUnits = new List<Unit>();

    public event Action<List<Unit>> UnitSpawned;

    public void SpawnUnits(IReadOnlyList<Cell> grid, Unit[] units)
    {
        Shuffle();

        int index = 0;

        foreach (Vector2Int spawnPoint in _spawnPoints)
        {
            foreach (Cell cell in grid)
            {
                if (cell.Coordinates == spawnPoint)
                {
                    Unit unit = Instantiate(units[index],
                        new Vector3(cell.transform.position.x, units[index].transform.position.y, cell.transform.position.z), Quaternion.identity, _parent);

                    unit.SetCoordinates(spawnPoint.x, spawnPoint.y);

                    _spawnedUnits.Add(unit);

                    index++;

                    break;
                }
            }
        }

        Debug.Log("Units Spawned");

        UnitSpawned?.Invoke(_spawnedUnits);
    }

    private void Shuffle()
    {
        for (int i = 0; i < _spawnPoints.Length - 1; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length - 1);

            Vector2Int tempValue = _spawnPoints[randomIndex];
            _spawnPoints[randomIndex] = _spawnPoints[i];
            _spawnPoints[i] = tempValue;
        }
    }
}
