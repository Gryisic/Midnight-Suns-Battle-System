using System;
using System.Collections.Generic;
using Factories;
using Units.Interfaces;
using UnityEngine;
using Utils;

namespace Actions
{
    [Serializable]
    public class SkillsContainer
    {
        [SerializeField] private List<SkillCutscene> _skillCutscenes;

        private List<Skill> _skills = new List<Skill>();
        
        public IReadOnlyList<Skill> Skills => _skills;

        public void Initialize(IUnitData unitData, Enums.UnitType unitType)
        {
            SkillsFactory skillsFactory = new SkillsFactory(unitData);
            skillsFactory.Load(unitType);
            
            skillsFactory.Create(_skillCutscenes, out IReadOnlyList<Skill> skills);
            _skills.AddRange(skills);
        }
    }
}