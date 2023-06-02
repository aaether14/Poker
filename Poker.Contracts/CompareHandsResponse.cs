namespace Poker.Contracts;

// A List<string> is the equivalent of a hand.
// We return a list of lists of hands to account for hands of equal rank.
// As such, the first list in the result is the list of all hands which tied for first position.
// the second list is for all hands tied for second position and so on and so forth.
public record CompareHandsResponse(List<List<List<string>>> Hands);