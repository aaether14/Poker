using Poker.Domain;
using Poker.Contracts;
using FluentValidation;
using Poker.HandsApi.Validators;
using FluentValidation.Results;
using Poker.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IValidator<CompareHandsRequest>, CompareHandsRequestValidator>();

var app = builder.Build();

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

app.MapGet("/api/v1/roll_hands", (int n) => 
{
    Deck deck = new Deck(new Random());
    List<List<string>> hands = Enumerable.Range(0, n)
        .Select(_ => deck.DealHand())
        .Select(hand => hand.Cards.Select(c => c.ToString()).ToList())
        .ToList();

    return new GetHandsResponse(hands);
});

app.MapPost("/api/v1/compare_hands", async (IValidator<CompareHandsRequest> validator, CompareHandsRequest request) => 
{   
    ValidationResult validationResult = await validator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    List<List<string>> hands = request.Hands
        .Select(handCards => new Hand(handCards.Select(c => Enum.Parse<Card>(c)).ToList()))
        .OrderDescending()
        .Select(hand => hand.Cards.Select(c => c.ToString()).ToList())
        .ToList();

    return Results.Ok(new GetHandsResponse(hands));
});

app.Run();
