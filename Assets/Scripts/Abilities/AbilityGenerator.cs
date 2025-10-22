using System.Collections.Generic;
using UnityEngine;

public class AbilityGenerator : MonoBehaviour
{
    [SerializeField] private Ability[] _availableAbilities;
    [SerializeField] private Grid _grid;

    private List<Ability> _spawnedAbilities;

    public void SpawnAbility()
    {
        _spawnedAbilities.Clear();


    }
}