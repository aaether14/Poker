namespace Poker.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Domain.Exceptions;

public class Deck
{
    private List<Card> _cards;
    private int _topCardIndex;
    private Random _random;

    public Deck(Random random)
    {
        _cards = Enum.GetValues<Card>().ToList();
        _topCardIndex = _cards.Count - 1;
        _random = random;
    }

    public Card Deal()
    {
        if (_topCardIndex < 0)
        {
            throw new AllCardsAlreadyDealtException("The deck is empty. All cards have been dealt.");
        }

        int index = _random.Next(0, _topCardIndex + 1);
        Card card = _cards[index];

        // Swap the selected card with the top card
        _cards[index] = _cards[_topCardIndex];
        _cards[_topCardIndex] = card;

        _topCardIndex--;
        return card;
    }

    public Hand DealHand()
    {
        List<Card> handCards = new List<Card>();

        for (int i = 0; i < Hand.CardsInHand; i++)
        {
            Card card = Deal();
            handCards.Add(card);
        }

        return new Hand(handCards);
    }
}
