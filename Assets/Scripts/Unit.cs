using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;

    public UnitMover UnitMover => _mover;
    public Vector2Int Coordinate {  get; private set; }

    public void SetCoordinates(int x, int y) =>
        Coordinate = new Vector2Int(x, y);
}
