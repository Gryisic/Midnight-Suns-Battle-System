using System.Collections.Generic;
using TargetSelection.Interfaces;
using UnityEngine;
using Utils;

namespace Actions
{
    [CreateAssetMenu(menuName = "Gameplay / Templates / Skill", fileName = "Skill")]
    public class SkillTemplate : ScriptableObject, ITargetSelectionData
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private float _distance;
        [SerializeField] private Affect[] _affects;

        [SerializeField] private Enums.TargetType _targetType;
        [SerializeField] private Enums.TargetSelectionType _targetSelectionType;
        [SerializeField] private int _targetsCount;
        
        [SerializeField] private bool _isCard;
        [SerializeField] private Enums.UnitType _holder;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Enums.SkillType _skillType;
        [SerializeField] private int _cost;
        [SerializeField] private int _add;
        [SerializeField] private int _copies = 1;

        public string Name => _name;
        public string Description => _description;
        public float Distance => _distance;

        public IReadOnlyList<Affect> Affects => _affects;

        public Enums.TargetType TargetType => _targetType;
        public Enums.TargetSelectionType TargetSelectionType => _targetSelectionType;
        public int TargetsCount => _targetsCount;

        public bool IsCard => _isCard;
        public Enums.UnitType Holder => _holder;
        public Sprite Icon => _icon;
        public Enums.SkillType SkillType => _skillType;
        public int Cost => _cost;
        public int Add => _add;
        public int Copies => _copies;
    }
}