using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PreparationStage _preparationStage;
    [SerializeField] private ActionStage _actionStage;
    [SerializeField] private MovementStage _movementStage;
    [SerializeField] private Grid _grid;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Unit[] _prefabs;

    private List<Unit> _units;

    private void Start() =>
        SpawnUnits();

    private void OnEnable()
    {
        _spawner.UnitSpawned += SetUnits;
        _preparationStage.PreparationStageFinished += StartMovingStage;
        _movementStage.MovementStageFinished += StartActionStage;
        _actionStage.ActionStageFinished += StartPreparationStage;
    }

    private void OnDisable()
    {
        _spawner.UnitSpawned -= SetUnits;
        _preparationStage.PreparationStageFinished -= StartMovingStage;
        _movementStage.MovementStageFinished -= StartActionStage;
        _actionStage.ActionStageFinished -= StartPreparationStage;
    }

    private void SetUnits(List<Unit> units)
    {
        _units = units;

        StartPreparationStage();
    }

    private void StartActionStage() =>
        _actionStage.StartUseActionAbilities();

    private void SpawnUnits() =>
        _spawner.SpawnUnits(_grid.CellsInGrid, _prefabs);

    private void StartPreparationStage() =>
        _preparationStage.Prepare(_units, _grid.CellsInGrid);

    private void StartMovingStage(IReadOnlyDictionary<Unit, Cell> unitsCell) =>
        _movementStage.MoveTo(unitsCell);
}
