using System;
using Units;

namespace Actions
{
    public class Skill
    {
        public event Func<Unit> RequestTarget; 
        
        public SkillTemplate Data { get; }
        public SkillCutscene Cutscene { get; }

        public Skill(SkillTemplate template, SkillCutscene cutscene)
        {
            Data = template;
            Cutscene = cutscene;
        }

        public void Execute()
        {
            Unit target = RequestTarget?.Invoke();

            for (var index = 0; index < Data.Affects.Count; index++)
            {
                Affect affect = Data.Affects[index];
                target.Affect(affect);
            }
        }
    }
}