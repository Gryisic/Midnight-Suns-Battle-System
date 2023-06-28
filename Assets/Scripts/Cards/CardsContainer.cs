using System.Collections.Generic;

namespace Cards
{
    public class CardsContainer
    {
        public IReadOnlyList<Card> Cards { get; }
        
        public CardsContainer(IReadOnlyList<Card> cards)
        {
            Cards = cards;
        }
    }
}