using System.Collections.Generic;
using Units;
using Units.Interfaces;

namespace TargetSelection
{
    public class AITargetSelector
    {
        private readonly IUnitData _unitData;
        
        public AITargetSelector(IUnitData unitData)
        {
            _unitData = unitData;
        }
        
        public Unit FindClosest(IReadOnlyList<Unit> selectFrom)
        {
            float closestDistance = 999;
            Unit closestUnit = selectFrom[0];
            
            for (var index = 0; index < selectFrom.Count; index++)
            {
                Unit unit = selectFrom[index];
                float distance = (unit.Transform.position - _unitData.Transform.position).magnitude;

                if (distance > closestDistance) 
                    continue;
                
                closestDistance = distance;
                closestUnit = unit;
            }

            return closestUnit;
        }
    }
}