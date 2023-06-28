using Actions;
using Cards.Interfaces;
using UnityEngine;
using Utils;

namespace Cards
{
    public class Card : ICardDataProvider
    {
        public string Name { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public Enums.SkillType SkillType { get; }
        public int Cost { get; }
        
        public Skill Skill { get; }

        public Card(Skill skill)
        {
            Name = skill.Data.Name;
            Description = skill.Data.Description;
            Icon = skill.Data.Icon;
            SkillType = skill.Data.SkillType;
            Cost = skill.Data.Cost;
            Skill = skill;
        }
    }
}