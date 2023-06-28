using System;
using UnityEngine;
using Utils;

namespace Actions
{
    [Serializable]
    public class Affect
    {
        [SerializeField] private Enums.SkillAffectType _affectType;
        [SerializeField] private int _amount;

        public Enums.SkillAffectType AffectType => _affectType;
        public int Amount => _amount;
    }
}