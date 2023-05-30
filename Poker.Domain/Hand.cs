namespace Poker.Domain;

using System;
using System.Collections.Generic;

public class Hand
{

    public const int CardsInHand = 5;

    public List<Card> Cards { get; }

    public Hand(List<Card> dealtCards)
    {
        if (dealtCards.Count != CardsInHand)
        {
            throw new ArgumentException("A hand must contain exactly 5 cards.", nameof(dealtCards));
        }

        Cards = new List<Card>(dealtCards);
    }

}