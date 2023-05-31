namespace Poker.Domain;

using System;
using System.Collections.Generic;

public class Hand : IComparable<Hand>
{

    public const int CardsInHand = 5;

    public List<Card> Cards { get; }

    public Hand(List<Card> dealtCards)
    {
        if (dealtCards.Distinct().Count() != CardsInHand)
        {
            throw new ArgumentException("A hand must contain exactly 5 distinct cards.", nameof(dealtCards));
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

        List<(int Count, int Rank)> countRanks = Cards
            .GroupBy(c => c.GetRank())
            .Select(g => (g.Count(), g.Key))
            .OrderDescending()
            .ToList();

        bool isStraight = Cards.Count == Hand.CardsInHand 
            && (maxRank - minRank == Hand.CardsInHand - 1);

        if (isStraight && isFlush)
        {
            return (HandType.StraightFlush, ComputeTieBreaker(maxRank));
        }
        else if (countRanks[0].Count == 4)
        {
            return (HandType.FourOfAKind, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else if (countRanks[0].Count == 3 && countRanks[1].Count == 2)
        {
            return (HandType.FullHouse, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else if (isFlush)
        {
            return (HandType.Flush, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else if (isStraight)
        {
            return (HandType.Straight, ComputeTieBreaker(maxRank));
        }
        else if (countRanks[0].Count == 3)
        {
            return (HandType.ThreeOfAKind, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else if (countRanks[0].Count == 2 && countRanks[1].Count == 2)
        {
            return (HandType.TwoPair, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else if (countRanks[0].Count == 2)
        {
             return (HandType.OnePair, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
        else 
        {
            return (HandType.HighCard, 
                ComputeTieBreaker(countRanks.Select(countRank => countRank.Rank).ToArray()));
        }
    }

    private int ComputeTieBreaker(params int[] values)
    {
        int tieBreaker = 0;
        int maxRank = Card.SA.GetRank() + 1;
        int power = 1;

        for (int i = values.Length - 1; i >= 0; i--)
        {
            tieBreaker += values[i] * power;
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