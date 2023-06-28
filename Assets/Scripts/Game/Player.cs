using System;
using System.Collections.Generic;
using Cards;
using Mouse.Interfaces;
using TargetSelection.Interfaces;
using Units;
using Units.Interfaces;
using UnityEngine;
using Utils;

namespace Game
{
    public class Player : IDisposable
    {
        private Hero _controllableUnit;
        private Enums.MouseState _lastState;
        private bool _isActive;
        private Card _selectedCard;

        public event Func<Enums.UnitType, Hero> RequestHero; 
        public event Action<Card> CardUsed;
        public event Action<IUnitData> UnitChanged;
        public event Action<Enums.UnitDataType, IUnitData> UnitDataChanged;
        public event Action<ITargetSelectionData> RequestTargetSelection;
        public event Action<Enums.RuleType, int> HeroismGained;
        public event Action<Enums.RuleType, int> HeroismUsed;
        public event Action<Enums.RuleType, int> SkillUsed;
        public event Action<Enums.RuleType, int> Moved;
        public event Action TargetsSelected;
        
        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
        
        public void Dispose()
        {
            _controllableUnit.HealthChanged -= OnUnitHealthChanged;
        }

        public void ChangeControllableUnit(Hero unit)
        {
            if (unit == null)
                throw new NullReferenceException("New unit is null");

            if (_controllableUnit != null)
                _controllableUnit.HealthChanged -= OnUnitHealthChanged;
            
            _controllableUnit = unit;
            _controllableUnit.HealthChanged += OnUnitHealthChanged;
            
            UnitChanged?.Invoke(_controllableUnit);
            UnitDataChanged?.Invoke(Enums.UnitDataType.Full, _controllableUnit);
        }

        public void OnMouseStateChanged(IMouseData data)
        {
            if (data.State != Enums.MouseState.Selected)
            {
                _lastState = data.State;
                return;
            }

            if (_lastState == Enums.MouseState.Move)
            {
                Moved?.Invoke(Enums.RuleType.Moves, 1);
                
                _controllableUnit.MoveTo(data.Position);
            }
            
            _controllableUnit.Act();
        }

        public void OnCardSelected(Card card)
        {
            if (card == _selectedCard)
                return;

            _selectedCard = card;
            
            RequestTargetSelection?.Invoke(_selectedCard.Skill.Data);
            
            Hero newHero = RequestHero?.Invoke(_selectedCard.Skill.Data.Holder);
            ChangeControllableUnit(newHero);
        }

        public void UseSkill(Queue<Unit> targets)
        {
            _controllableUnit.UpdateTargets(targets);
            
            foreach (var target in targets)
            {
                float distance = (target.Transform.position - _controllableUnit.Transform.position).magnitude;
                
                if (distance > _selectedCard.Skill.Data.Distance)
                {
                    Vector3 newPosition = Vector3.MoveTowards(target.Transform.position, _controllableUnit.Transform.position, _selectedCard.Skill.Data.Distance);
                    
                    _controllableUnit.MoveTo(newPosition);
                }
                
                _controllableUnit.UseSkill(_selectedCard.Skill);
            }

            if (_selectedCard.Skill.Data.Cost > 0)
                HeroismUsed?.Invoke(Enums.RuleType.Heroism, _selectedCard.Skill.Data.Cost);
            if (_selectedCard.Skill.Data.Add > 0)
                HeroismGained?.Invoke(Enums.RuleType.Heroism, _selectedCard.Skill.Data.Add);
            
            CardUsed?.Invoke(_selectedCard);
            SkillUsed?.Invoke(Enums.RuleType.CardPlays, 1);
            TargetsSelected?.Invoke();
        }
        
        private void OnUnitHealthChanged(int currentAmount, int maxAmount) => UnitDataChanged?.Invoke(Enums.UnitDataType.HealthOnly, _controllableUnit);
    }
}