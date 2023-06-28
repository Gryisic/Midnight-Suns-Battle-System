using System.Collections.Generic;
using Actions;
using Utils;

namespace TargetSelection.Interfaces
{
    public interface ITargetSelectionData
    {
        IReadOnlyList<Affect> Affects { get; }
        Enums.TargetSelectionType TargetSelectionType { get; }
        int TargetsCount { get; }
    }
}