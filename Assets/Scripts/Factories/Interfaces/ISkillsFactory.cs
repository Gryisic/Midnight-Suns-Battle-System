using System.Collections.Generic;
using Actions;
using Utils;

namespace Factories.Interfaces
{
    public interface ISkillsFactory
    {
        void Load(Enums.UnitType unitType);
        
        void Create(IReadOnlyList<SkillCutscene> cutscenes, out IReadOnlyList<Skill> skills);
    }
}