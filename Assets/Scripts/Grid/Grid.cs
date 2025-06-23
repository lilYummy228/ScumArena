using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private List<Cell> _cells;

    public IReadOnlyList<Cell> CellsInGrid => _cells;
}
