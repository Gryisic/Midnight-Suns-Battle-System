using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using Factories.Interfaces;
using Units.Interfaces;
using UnityEngine;
using Utils;

namespace Factories
{
    public class SkillsFactory : ISkillsFactory
    {
        private readonly IUnitData _unitData;
        private SkillTemplate[] _templates;

        public SkillsFactory(IUnitData unitData)
        {
            _unitData = unitData;
        }

        public void Load(Enums.UnitType unitType)
        {
            _templates = Resources.LoadAll<SkillTemplate>($"{Constants.PathToUnits}/{unitType}/Skills");
            
            if (_templates.Length <= 0)
                throw new ArgumentException($"Cannot find skills of that unit: {unitType}");
        }

        public void Create(IReadOnlyList<SkillCutscene> cutscenes, out IReadOnlyList<Skill> skills)
        {
            List<SkillCutscene> scenes = cutscenes.ToList();
            List<Skill> newSkills = new List<Skill>();

            foreach (var skillTemplate in _templates)
            {
                SkillCutscene cutscene = scenes.Find(c => c.Name == skillTemplate.Name);
                Skill skill = new Skill(skillTemplate, cutscene);
                
                newSkills.Add(skill);
            }

            skills = newSkills;
        }
    }
}