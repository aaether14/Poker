using Poker.Domain;
using Poker.Contracts;
using Poker.HandsApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJsonOptions();

var app = builder.Build();

app.MapGet("/api/v1/roll_hands", (int n) => 
{
    Deck deck = new Deck(new Random());
    List<List<Card>> hands = Enumerable.Range(0, n)
        .Select(_ => deck.DealHand())
        .Select(hand => hand.Cards)
        .ToList();

    return new GetHandsResponse(hands);
});

app.MapPost("/api/v1/compare_hands", (CompareHandsRequest request) => 
{   
    List<List<Card>> hands = request.Hands
        .Select(handCards => new Hand(handCards))
        .OrderDescending()
        .Select(hand => hand.Cards)
        .ToList();

    return new GetHandsResponse(hands);
});

app.Run();
