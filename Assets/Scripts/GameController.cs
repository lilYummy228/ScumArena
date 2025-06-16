using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PreparationStage _preparationStage;
    [SerializeField] private Unit _player;
    [SerializeField] private Grid _grid;

    private void Awake()
    {
        _grid.GridGenerator.GenerateGrid();
        _player.SetCoordinates(0, 0);

        StartPraparationStage();
    }

    private void OnEnable()
    {
        _preparationStage.PreparationStageFinished += StartMovingStage;
        _player.UnitMover.Moved += StartPraparationStage;
    }

    private void OnDisable()
    {
        _preparationStage.PreparationStageFinished -= StartMovingStage;
        _player.UnitMover.Moved -= StartPraparationStage;
    }

    private void StartPraparationStage() =>
        _preparationStage.Prepare(_player, _grid.CellInGrid);

    private void StartMovingStage(Cell cell)
    {
        _player.UnitMover.MoveTo(cell);
        _player.SetCoordinates(cell.Coordinates.x, cell.Coordinates.y);
    }
}
