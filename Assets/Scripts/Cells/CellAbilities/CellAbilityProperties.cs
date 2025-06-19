using UnityEngine;

[CreateAssetMenu(fileName = "New Cell Ability", menuName = "Scriptable Objects/Cell Ability")]
public class CellAbilityProperties : ScriptableObject
{
    [Header("Visual")]
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [Header("Values")]
    [SerializeField][Range(-100, 100)] private float _healthValueChange = 0;
    [SerializeField][Range(0, 3)] private int _duration;
}
