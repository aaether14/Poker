namespace Poker.Domain;

using System;
using System.Collections.Generic;
using Poker.Domain.Exceptions;

public class Hand : IComparable<Hand>
{

    public const int CardsInHand = 5;

    public List<Card> Cards { get; }

    public Hand(List<Card> dealtCards)
    {
        if (dealtCards.Distinct().Count() != CardsInHand)
        {
            throw new InvalidHandException("A hand must contain exactly 5 distinct cards.");
        }

        Cards = new List<Card>(dealtCards);
    }

    public (HandType HandType, int TieBreaker) GetRank()
    {
        bool isFlush = Cards
            .Select(c => c.GetSuit())
            .Distinct()
            .Count() == 1;

        int minRank = Cards
            .Select(c => c.GetRank())
            .Min();
        int maxRank = Cards
            .Select(c => c.GetRank())
            .Max();

        bool isStraight = Cards.Count == Hand.CardsInHand 
            && (maxRank - minRank == Hand.CardsInHand - 1);

        // Group cards by rank and sort them such that cards which there are more of come first.
        // On equal counts, higher ranked cards come first.
        List<(int Count, int Rank)> countAndRankPairs = Cards
            .GroupBy(c => c.GetRank())
            .Select(g => (g.Count(), g.Key))
            .OrderDescending()
            .ToList();

        if (isStraight && isFlush)
        {
            return (HandType.StraightFlush, maxRank);
        }
        else if (countAndRankPairs[0].Count == 4)
        {
            return (HandType.FourOfAKind, ComputeTieBreaker(countAndRankPairs));
        }
        else if (countAndRankPairs[0].Count == 3 && countAndRankPairs[1].Count == 2)
        {
            return (HandType.FullHouse, ComputeTieBreaker(countAndRankPairs));
        }
        else if (isFlush)
        {
            return (HandType.Flush, ComputeTieBreaker(countAndRankPairs));
        }
        else if (isStraight)
        {
            return (HandType.Straight, maxRank);
        }
        else if (countAndRankPairs[0].Count == 3)
        {
            return (HandType.ThreeOfAKind, ComputeTieBreaker(countAndRankPairs));
        }
        else if (countAndRankPairs[0].Count == 2 && countAndRankPairs[1].Count == 2)
        {
            return (HandType.TwoPair, ComputeTieBreaker(countAndRankPairs));
        }
        else if (countAndRankPairs[0].Count == 2)
        {
             return (HandType.OnePair, ComputeTieBreaker(countAndRankPairs));
        }
        else 
        {
            return (HandType.HighCard, ComputeTieBreaker(countAndRankPairs));
        }
    }

    // Combine all values into a composite tiebreaker by using the "Card.SA.GetRank() + 1" number base .
    // This means that tiebreaker values which are deemed more important always trump those considered less important.
    private int ComputeTieBreaker(List<(int Count, int Rank)> countAndRankPairs)
    {
        int tieBreaker = 0;
        int maxRank = Card.SA.GetRank() + 1;
        int power = 1;

        for (int i = countAndRankPairs.Count - 1; i >= 0; i--)
        {
            tieBreaker += countAndRankPairs[i].Rank * power;
            power *= maxRank;
        }

        return tieBreaker;
    }

    public int CompareTo(Hand? other)
    {
        if (other is null)
        {
            return 1;
        }

        return GetRank().CompareTo(other.GetRank());
    }
}