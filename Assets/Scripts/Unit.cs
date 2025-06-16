using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int _movementRange = 2;

    public int MovementRange => _movementRange;
    public Vector2Int Coordinate {  get; private set; }

    public void SetCoordinates(int x, int y) =>
        Coordinate = new Vector2Int(x, y);
}
