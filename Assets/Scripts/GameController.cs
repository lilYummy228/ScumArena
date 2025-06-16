using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PreparationStage _preparationStage;
    [SerializeField] private ActionStage _actionStage;
    [SerializeField] private MovementStage _movementStage;
    [SerializeField] private Grid _grid;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Unit[] _prefabs;

    private Unit _player;

    private void Start() =>
        _grid.GridGenerator.GenerateGrid();

    private void OnEnable()
    {
        _grid.GridSet += SpawnUnits;
        _spawner.UnitSpawned += SetPlayer;
        _preparationStage.PreparationStageFinished += StartMovingStage;
        _movementStage.MovementStageFinished += StartActionStage;
        _actionStage.ActionStageFinished += StartPreparationStage;
    }

    private void OnDisable()
    {
        _grid.GridSet -= SpawnUnits;
        _spawner.UnitSpawned -= SetPlayer;
        _preparationStage.PreparationStageFinished -= StartMovingStage;
        _movementStage.MovementStageFinished -= StartActionStage;
        _actionStage.ActionStageFinished -= StartPreparationStage;
    }

    private void SetPlayer(Unit player)
    {
        _player = player;

        StartPreparationStage();
    }

    private void StartActionStage() =>
        _actionStage.StartUseActionAbilities();

    private void SpawnUnits() =>
        _spawner.SpawnUnits(_grid.CellsInGrid, _prefabs);

    private void StartPreparationStage()
    {
        if (_player != null)
            _preparationStage.Prepare(_player, _grid.CellsInGrid);
    }

    private void StartMovingStage(Cell cell) =>
        _movementStage.MoveTo(_player, cell);
}
