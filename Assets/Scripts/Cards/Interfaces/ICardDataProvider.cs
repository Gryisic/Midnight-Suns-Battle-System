using UnityEngine;
using Utils;

namespace Cards.Interfaces
{
    public interface ICardDataProvider
    {
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        Enums.SkillType SkillType { get; }
        int Cost { get; }
    }
}