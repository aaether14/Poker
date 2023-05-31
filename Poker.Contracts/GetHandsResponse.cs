using Poker.Domain;

namespace Poker.Contracts;

public record GetHandsResponse(List<List<Card>> Hands);