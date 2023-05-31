using Poker.Domain;

namespace Poker.Contracts;

public record CompareHandsRequest(List<List<Card>> Hands);