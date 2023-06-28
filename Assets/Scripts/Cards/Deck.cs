using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Cards
{
    public class Deck : IDisposable
    {
        private readonly Queue<Card> _usedCards = new Queue<Card>();
        private readonly List<Card> _activeCards;

        public Hand Hand { get; } = new Hand();
        
        public Deck(IReadOnlyList<Card> cards)
        {
            _activeCards = cards.ToList();
        }

        public void Initialize()
        {
            Hand.RequestCards += GetCards;
        }

        public void Dispose()
        {
            Hand.RequestCards -= GetCards;
        }
        
        private void RefreshCards()
        {
            List<Card> usedCards = _usedCards.ToList();

            foreach (var card in usedCards)
                _activeCards.Add(_usedCards.Dequeue());
            
            Shuffle();
        }

        private void Shuffle()
        {
            System.Random random = new System.Random();
            
            for (int i = _activeCards.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (_activeCards[j], _activeCards[i]) = (_activeCards[i], _activeCards[j]);
            }
        }
        
        private IReadOnlyList<Card> GetCards(int amount)
        {
            List<Card> cards = new List<Card>();

            if (_activeCards.Count < amount)
                RefreshCards();
            
            for (int i = 0; i < amount; i++)
            {
                int randomIndex = Random.Range(0, _activeCards.Count);
                Card card = _activeCards[randomIndex];

                _activeCards.Remove(card);
                _usedCards.Enqueue(card);
                cards.Add(card);
            }

            return cards;
        }
    }
}