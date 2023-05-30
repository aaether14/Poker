using Microsoft.AspNetCore.Http.Json;
using Poker.Domain;
using Poker.JsonConverters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options => 
{
    options.SerializerOptions.Converters.Add(new EnumStringConverter<Card>());
});

var app = builder.Build();

app.MapGet("/api/v1/hands", (int n) => 
{
    Deck deck = new Deck(new Random());
    List<Hand> hands = Enumerable.Range(0, n).Select(_ => deck.DealHand()).ToList();
    return hands;
});

app.Run();
