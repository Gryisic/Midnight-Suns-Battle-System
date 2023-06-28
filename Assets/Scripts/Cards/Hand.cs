using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using Turns.Interfaces;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Cards
{
    public class Hand
    {
        public event Func<int, IReadOnlyList<Card>> RequestCards;
        public event Func<IRulesData> RequestRulesData; 
        public event Action<ICardDataProvider> CardAdded;
        public event Action<Card> CardSelected;
        public event Action<int> CardRemoved;
        public event Action<Enums.RuleType, int> Redrawed;
        public event Action CardDeselected;
        public event Action Cleared;

        private readonly List<Card> _cards = new List<Card>();

        public Card SelectedCard { get; private set; }

        public void SelectCard(int index)
        {
            SelectedCard = _cards[index];
            
            IRulesData rulesData = RequestRulesData?.Invoke();
            
            if (SelectedCard.Skill.Data.Cost > rulesData.Heroism)
            {
                DeselectCard();
                return;
            }
            
            CardSelected?.Invoke(SelectedCard);
        }

        public void DeselectCard()
        {
            SelectedCard = null;
            
            CardDeselected?.Invoke();
        }

        public void RedrawCard(int index)
        {
            Redrawed?.Invoke(Enums.RuleType.Redraws, 1);

            Card card = _cards[index];
            Remove(card);
            
            IReadOnlyList<Card> cards = RequestCards?.Invoke(1);
            Add(cards.First());
        }
        
        public void UpdateCards()
        {
            if (_cards.Count >= Constants.MaxAmountCardsInHand) 
                return;
            
            int amount = Mathf.Clamp(Random.Range(1, Constants.MaxAmountCardsInHand - _cards.Count), 1, 5);
            IReadOnlyList<Card> cards = RequestCards?.Invoke(amount);

            for (var i = 0; i < cards.Count; i++)
            {
                Card card = cards[i];
                Add(card);
            }
        }
        
        public void Clear()
        {
            _cards.Clear();
            Cleared?.Invoke();
        }
        
        public void Add(Card card)
        {
            if (_cards.Count + 1 > Constants.MaxAmountCardsInHand)
                throw new InvalidOperationException("Trying to add card that exceeds maximal hand capacity");
            
            _cards.Add(card);
            CardAdded?.Invoke(card);
        }

        public void Remove(Card card)
        {
            if (_cards.Count <= 0)
                throw new InvalidOperationException("Trying to remove card from empty hand");

            if (_cards.Contains(card) == false) 
                throw new InvalidOperationException("Trying to remove card that not in hand");
            
            int index = Array.IndexOf(_cards.ToArray(), card);
            _cards.Remove(card);
            CardRemoved?.Invoke(index);
        }
    }
}