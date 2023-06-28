using System;
using Turns.Interfaces;
using UnityEngine;
using Utils;

namespace Turns
{
    public class Rules : IRulesData
    {
        private const int MaxRule = 5;
        private const int DefaultHeroism = 3;
        private const int DefaultCardPlays = 3;
        private const int DefaultRedraws = 2;
        private const int DefaultMoves = 1;

        public event Action<Enums.RuleType, int> ValueUpdated; 

        public int CardPlays { get; private set; }
        public int Redraws { get; private set; }
        public int Moves { get; private set; }
        public int Heroism { get; private set; }

        public IRulesData GetData() => this;

        public void Initialize()
        {
            Heroism = DefaultHeroism;

            RestoreDefaults();
        }

        public void RestoreDefaults()
        {
            CardPlays = DefaultCardPlays;
            Redraws = DefaultRedraws;
            Moves = DefaultMoves;
            
            ValueUpdated?.Invoke(Enums.RuleType.CardPlays, CardPlays);
            ValueUpdated?.Invoke(Enums.RuleType.Redraws, Redraws);
            ValueUpdated?.Invoke(Enums.RuleType.Moves, Moves);
            ValueUpdated?.Invoke(Enums.RuleType.Heroism, Heroism);
        }

        public void IncreaseValue(Enums.RuleType ruleType, int amount = 1)
        {
            if (amount < 0)
                throw new InvalidOperationException("Trying to add negative value");
            
            ChangeValue(ruleType, amount);
        }
        
        public void DecreaseValue(Enums.RuleType ruleType, int amount = 1)
        {
            if (amount < 0)
                throw new InvalidOperationException("Trying to subtract negative value");

            ChangeValue(ruleType, amount * -1);
        }
        
        private void ChangeValue(Enums.RuleType ruleType, int amount)
        {
            int changedValue;
            
            switch (ruleType)
            {
                case Enums.RuleType.CardPlays:
                    CardPlays = Mathf.Clamp(CardPlays + amount, 0, MaxRule);
                    changedValue = CardPlays;
                    break;
                
                case Enums.RuleType.Redraws:
                    Redraws = Mathf.Clamp(Redraws + amount, 0, MaxRule);
                    changedValue = Redraws;
                    break;
                
                case Enums.RuleType.Moves:
                    Moves = Mathf.Clamp(Moves + amount, 0, MaxRule);
                    changedValue = Moves;
                    break;
                
                case Enums.RuleType.Heroism:
                    Heroism = Mathf.Clamp(Heroism + amount, 0, Constants.MaxHeroismAmount);
                    changedValue = Heroism;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(ruleType), ruleType, null);
            }
            
            ValueUpdated?.Invoke(ruleType, changedValue);
        }
    }
}