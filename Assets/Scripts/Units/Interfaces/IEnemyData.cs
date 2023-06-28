using System;

namespace Units.Interfaces
{
    public interface IEnemyData : IUnitData
    {
        event Action<Unit> TargetUpdated; 
    }
}