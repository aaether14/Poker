using Poker.Domain;
using Poker.Contracts;
using FluentValidation;
using Poker.HandsApi.Validators;
using FluentValidation.Results;
using Poker.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<DescribeHandRequest>, DescribeHandRequestValidator>();
builder.Services.AddScoped<IValidator<CompareHandsRequest>, CompareHandsRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception handler.
app.Use(async (context, next) => 
{
    try
    {
        await next.Invoke(context);
    }
    catch (DomainException domainException) 
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Title = domainException.GetType().ToString(),
            Detail = domainException.Message
        });
    }
    catch (Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new ProblemDetails()
        {
            Title = "Internal server error.",
            Detail = exception.Message
        });
    }
});

var handsApi = app.MapGroup("api/v1/hands");

handsApi.MapGet("/roll", (int n) => 
{
    Deck deck = new Deck(new Random());
    List<List<string>> hands = Enumerable.Range(0, n)
        .Select(_ => deck.DealHand())
        .Select(hand => hand.Cards.Select(c => c.ToString()).ToList())
        .ToList();

    return new RollHandsResponse(hands);
})
.WithName("RollHands")
.Produces<RollHandsResponse>()
.WithOpenApi();

handsApi.MapPost("/describe", async (IValidator<DescribeHandRequest> validator, DescribeHandRequest request) => 
{
    ValidationResult validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var (handType, tieBreaker) = new Hand(request.Hand.Select(c => Enum.Parse<Card>(c)).ToList())
        .GetRank();

    return Results.Ok(new DescribeHandResponse(handType.ToString(), tieBreaker));    
})
.WithName("DescribeHand")
.Produces<DescribeHandResponse>()
.WithOpenApi();

handsApi.MapPost("/compare", async (IValidator<CompareHandsRequest> validator, CompareHandsRequest request) => 
{   
    ValidationResult validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    List<Hand> hands = request.Hands
        .Select(handCards => new Hand(handCards.Select(c => Enum.Parse<Card>(c)).ToList()))
        .ToList();

    // A List<string> is the equivalent of a hand.
    // We return a list of lists of hands to account for hands of equal rank.
    // As such, the first list in the result is the list of all hands which tied for first position.
    // the second list is for all hands tied for second position and so on and so forth.
    List<List<List<string>>> result = hands.GroupBy(hand => hand.GetRank())
        .OrderByDescending(g => g.Key)
        .Select(g => g.Select(h => h.Cards.Select(c => c.ToString()).ToList()).ToList())
        .ToList();

    return Results.Ok(new CompareHandsResponse(result));
})
.WithName("CompareHands")
.Produces<CompareHandsResponse>()
.WithOpenApi();

app.Run();
