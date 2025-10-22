using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilityGenerator : MonoBehaviour
{
    [SerializeField] private Ability[] _availableAbilities;
    [SerializeField] private Grid _grid;

    private Dictionary<Ability, int> _spawnedAbilitiesX = new Dictionary<Ability, int>();

    public void SpawnAbility()
    {
        _spawnedAbilitiesX.Clear();

        for (int i = 0; i < _grid.GridSizeX; i++)
            _spawnedAbilitiesX.Add(GetRandomAbility(), i);
    }

    private Ability GetRandomAbility() =>
        _availableAbilities[Random.Range(0, _availableAbilities.Length - 1)];
}