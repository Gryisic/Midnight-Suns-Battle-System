using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Units
{
    public class Hero : Unit
    {
        public CardsContainer CardsContainer { get; private set; }
        
        public override void Initialize()
        {
            base.Initialize();

            FillCardsContainer();
        }

        private void FillCardsContainer()
        {
            List<Card> cards = new List<Card>();
            
            foreach (var skill in skillsContainer.Skills)
            {
                for (int i = 0; i < skill.Data.Copies; i++)
                {
                    cards.Add(new Card(skill));
                }
            }
            
            CardsContainer = new CardsContainer(cards);
        }
    }
}