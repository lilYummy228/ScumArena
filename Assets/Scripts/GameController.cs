using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PreparationStage _unitMovementTimer;

    private void StartGame()
    {
        _unitMovementTimer.StartPreparationStage();
    }
}
