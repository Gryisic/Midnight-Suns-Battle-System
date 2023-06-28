using System;
using System.Collections.Generic;
using Actions;
using TargetSelection;
using UI;
using Units.Interfaces;
using UnityEngine;

namespace Units
{
    public class Enemy : Unit, IEnemyData
    {
        [SerializeField] private EnemyTargetIconView _targetIconView;
        
        private AITargetSelector _targetSelector;
        private Unit _target;

        public event Action<Unit> TargetUpdated; 

        public override void Initialize()
        {
            base.Initialize();

            _targetSelector = new AITargetSelector(this);
        }

        public void UpdateTarget(UnitsHandler unitsHandler)
        {
            _target = _targetSelector.FindClosest(unitsHandler.Heroes);

            Queue<Unit> targets = new Queue<Unit>();
            targets.Enqueue(_target);

            if (_target is Hero hero)
                _targetIconView.UpdateIcon(hero.Icon);
            
            UpdateTargets(targets);

            TargetUpdated?.Invoke(_target);
        }

        public void UseRandomSkill()
        {
            Skill skill = skillsContainer.Skills[UnityEngine.Random.Range(0, skillsContainer.Skills.Count)];
            float distance = (_target.Transform.position - Transform.position).magnitude;

            if (distance > skill.Data.Distance)
            {
                Vector3 position = Vector3.MoveTowards(_target.Transform.position, Transform.position, skill.Data.Distance);
                
                MoveTo(position);
            }
            
            UseSkill(skill);
        }
    }
}