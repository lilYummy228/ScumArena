using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PreparationStage _preparationStage;
    [SerializeField] private UnitMover _unitMover;
    [SerializeField] private GridGenerator _gridGenerator;

    private void Awake()
    {
        _gridGenerator.GenerateGrid();

        StartPraparationStage();
    }

    private void OnEnable()
    {
        _preparationStage.PreparationStageFinished += StartMovingStage;
        _unitMover.Moved += StartPraparationStage;
    }

    private void OnDisable()
    {
        _preparationStage.PreparationStageFinished -= StartMovingStage;
        _unitMover.Moved -= StartPraparationStage;
    }

    private void StartPraparationStage() =>
        _preparationStage.Prepare();

    private void StartMovingStage(Cell cell) =>
        _unitMover.MoveTo(cell);
}
