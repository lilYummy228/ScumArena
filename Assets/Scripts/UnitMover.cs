using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private Transform _unit;
    [SerializeField] private PreparationStage _preparationStage;

    private void OnEnable() =>
        _preparationStage.PreparationStageFinished += MoveTo;

    private void OnDisable() =>
        _preparationStage.PreparationStageFinished -= MoveTo;

    public void MoveTo(Cell cell)
    {
        _unit.position = new Vector3(cell.transform.position.x, _unit.position.y, cell.transform.position.z);
    }
}
