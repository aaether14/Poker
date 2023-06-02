using System.Collections.Generic;
using Xunit;

namespace Poker.Domain.Tests;

public class HandTests
{

    [Fact]
    public void CompareTo_Returns_Greater_When_HighCard_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.H2, Card.C3, Card.D4, Card.SJ });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.H2, Card.C3, Card.D4, Card.SJ });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.HighCard, hand1.GetRank().HandType);
        Assert.Equal(HandType.HighCard, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_OnePair_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.HA, Card.C2, Card.D3, Card.SJ });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.HK, Card.C3, Card.D4, Card.SJ });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.OnePair, hand1.GetRank().HandType);
        Assert.Equal(HandType.OnePair, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_TwoPair_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.HA, Card.C2, Card.D2, Card.SJ });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.HK, Card.C3, Card.D3, Card.SJ });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.TwoPair, hand1.GetRank().HandType);
        Assert.Equal(HandType.TwoPair, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_ThreeOfAKind_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.HA, Card.CA, Card.D2, Card.SJ });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.HK, Card.CK, Card.D3, Card.SJ });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.ThreeOfAKind, hand1.GetRank().HandType);
        Assert.Equal(HandType.ThreeOfAKind, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_Straight_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.HQ, Card.HK, Card.CJ, Card.DT, Card.S9 });
        var hand2 = new Hand(new List<Card> { Card.ST, Card.H9, Card.C8, Card.D7, Card.S6 });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.Straight, hand1.GetRank().HandType);
        Assert.Equal(HandType.Straight, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_Flush_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.S2, Card.S3, Card.S4, Card.SJ });
        var hand2 = new Hand(new List<Card> { Card.HA, Card.H2, Card.H3, Card.H4, Card.H5 });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.Flush, hand1.GetRank().HandType);
        Assert.Equal(HandType.Flush, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_FullHouse_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.HA, Card.CA, Card.S2, Card.H2 });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.HK, Card.CK, Card.S3, Card.H3 });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.FullHouse, hand1.GetRank().HandType);
        Assert.Equal(HandType.FullHouse, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_FourOfAKind_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.SA, Card.HA, Card.CA, Card.DA, Card.S2 });
        var hand2 = new Hand(new List<Card> { Card.SK, Card.HK, Card.CK, Card.DK, Card.S3 });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.FourOfAKind, hand1.GetRank().HandType);
        Assert.Equal(HandType.FourOfAKind, hand2.GetRank().HandType);
    }

    [Fact]
    public void CompareTo_Returns_Greater_When_StraightFlush_Hand_Is_Better()
    {
        // Arrange
        var hand1 = new Hand(new List<Card> { Card.HJ, Card.HQ, Card.HK, Card.HA, Card.HT });
        var hand2 = new Hand(new List<Card> { Card.ST, Card.S9, Card.S8, Card.S7, Card.S6 });

        // Act
        var result = hand1.CompareTo(hand2);

        // Assert
        Assert.Equal(1, result);
        Assert.Equal(HandType.StraightFlush, hand1.GetRank().HandType);
        Assert.Equal(HandType.StraightFlush, hand2.GetRank().HandType);
    }



}
