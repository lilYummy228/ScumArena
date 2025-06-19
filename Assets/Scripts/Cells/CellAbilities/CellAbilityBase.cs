using UnityEngine;

public abstract class CellAbilityBase : MonoBehaviour
{
    [SerializeField] private CellAbilityProperties _properties;

    public abstract void Action();
}
