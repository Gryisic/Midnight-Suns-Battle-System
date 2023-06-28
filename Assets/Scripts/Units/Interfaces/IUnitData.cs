using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Units.Interfaces
{
    public interface IUnitData
    {
        string Name { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        Sprite Icon { get; }
        Transform Transform { get; }
        Enums.UnitType Type { get; }
        IReadOnlyList<Unit> Targets { get; }
    }
}