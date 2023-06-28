using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using UI;
using Units.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Action = Actions.Action;

namespace Units
{
    public abstract class Unit : MonoBehaviour, IUnitData, IDisposable
    {
        [SerializeField] protected SkillsContainer skillsContainer;
        [SerializeField] protected ParticlesHandler particlesHandler;
        [SerializeField] protected UnitAnimator animator;
        [SerializeField] protected Enums.UnitType type;

        [FormerlySerializedAs("_localHealthBar")] [Space] [SerializeField] private LocalHealthBarView localHealthBarView;
        
        [Space][SerializeField] private Sprite _icon;
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        private List<Unit> _targets = new List<Unit>();

        public event Action<IUnitData, Action> AddActionToQueue;
        public event Action<int, int> HealthChanged; 
        public event System.Action ExecuteActionsInQueue;

        public SkillsContainer SkillsContainer => skillsContainer;
        public string Name => name;
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;

                HealthChanged?.Invoke(CurrentHealth, MaxHealth);
            }
        }
        public int MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        public Transform Transform => transform;
        public Enums.UnitType Type => type;

        public IReadOnlyList<Unit> Targets => _targets;

        public virtual void Initialize()
        {
            _currentHealth = _maxHealth;
            
            skillsContainer.Initialize(this, type);

            HealthChanged += localHealthBarView.UpdateAmount;
            particlesHandler.SetOnTarget += SetParticleOnTarget;
            
            foreach (var skill in skillsContainer.Skills)
            {
                skill.RequestTarget += GetTarget;
                skill.Cutscene.RequestParticleActivation += particlesHandler.PlayParticle;
            }
        }

        public void Dispose()
        {
            HealthChanged -= localHealthBarView.UpdateAmount;
            particlesHandler.SetOnTarget -= SetParticleOnTarget;
            
            foreach (var skill in skillsContainer.Skills)
            {
                skill.RequestTarget -= GetTarget;
                skill.Cutscene.RequestParticleActivation -= particlesHandler.PlayParticle;
            }
        }

        public void Act() => ExecuteActionsInQueue?.Invoke();
        
        public void MoveTo(Vector3 position)
        {
            Action action = new MoveAction(transform, position, animator);
            
            AddActionToQueue?.Invoke(this, action);
        }

        public void UseSkill(Skill skill)
        {
            Action action = new SkillAction(skill, Transform);
            
            AddActionToQueue?.Invoke(this, action);
        }

        public void Affect(Affect affect)
        {
            switch (affect.AffectType)
            {
                case Enums.SkillAffectType.DealDamage:
                    ApplyDamage(affect.Amount);
                    break;
                
                case Enums.SkillAffectType.Heal:
                    Heal(affect.Amount);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateTargets(Queue<Unit> targets) => _targets = targets.ToList();

        private void SetParticleOnTarget(ParticleTransformMap particle) 
        {
            Transform particleTransform = particle.Transform;
            
            particleTransform.SetParent(_targets[0].Transform);
            particleTransform.localPosition = Vector3.zero;
            particleTransform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        
        private void ApplyDamage(int amount)
        {
            CurrentHealth = Mathf.Clamp(_currentHealth - amount, 0, MaxHealth);
            
            animator.PlayStandardAnimation(Enums.StandardAnimationType.TakeDamage);
        }

        private void Heal(int amount) => CurrentHealth = Mathf.Clamp(_currentHealth + amount, 0, MaxHealth);
        
        private Unit GetTarget() => _targets[0];
    }
}