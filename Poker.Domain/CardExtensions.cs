namespace Poker.Domain;

public static class CardExtensions
{
    public static int GetRank(this Card card) =>
        card switch
        {
            Card.S2 or Card.H2 or Card.D2 or Card.C2 => 2,
            Card.S3 or Card.H3 or Card.D3 or Card.C3 => 3,
            Card.S4 or Card.H4 or Card.D4 or Card.C4 => 4,
            Card.S5 or Card.H5 or Card.D5 or Card.C5 => 5,
            Card.S6 or Card.H6 or Card.D6 or Card.C6 => 6,
            Card.S7 or Card.H7 or Card.D7 or Card.C7 => 7,
            Card.S8 or Card.H8 or Card.D8 or Card.C8 => 8,
            Card.S9 or Card.H9 or Card.D9 or Card.C9 => 9,
            Card.ST or Card.HT or Card.DT or Card.CT => 10,
            Card.SJ or Card.HJ or Card.DJ or Card.CJ => 11,
            Card.SQ or Card.HQ or Card.DQ or Card.CQ => 12,
            Card.SK or Card.HK or Card.DK or Card.CK => 13,
            Card.SA or Card.HA or Card.DA or Card.CA => 14,
            _ => throw new ArgumentOutOfRangeException(nameof(card), "Invalid card value.")
        };

    public static CardSuit GetColor(this Card card) =>
        card switch
        {
            Card.SA or Card.S2 or Card.S3 or Card.S4 or Card.S5 or Card.S6 or Card.S7 or Card.S8 or Card.S9 or Card.ST or Card.SJ or Card.SQ or Card.SK => CardSuit.Spades,
            Card.HA or Card.H2 or Card.H3 or Card.H4 or Card.H5 or Card.H6 or Card.H7 or Card.H8 or Card.H9 or Card.HT or Card.HJ or Card.HQ or Card.HK => CardSuit.Hearts,
            Card.DA or Card.D2 or Card.D3 or Card.D4 or Card.D5 or Card.D6 or Card.D7 or Card.D8 or Card.D9 or Card.DT or Card.DJ or Card.DQ or Card.DK => CardSuit.Diamonds,
            Card.CA or Card.C2 or Card.C3 or Card.C4 or Card.C5 or Card.C6 or Card.C7 or Card.C8 or Card.C9 or Card.CT or Card.CJ or Card.CQ or Card.CK => CardSuit.Clubs,
            _ => throw new ArgumentOutOfRangeException(nameof(card), "Invalid card value.")
        };
}
