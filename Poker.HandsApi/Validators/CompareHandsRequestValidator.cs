using FluentValidation;
using FluentValidation.Results;
using Poker.Contracts;
using Poker.Domain;

namespace Poker.HandsApi.Validators;

public class CompareHandsRequestValidator : AbstractValidator<CompareHandsRequest>
{
    public CompareHandsRequestValidator()
    {
        RuleFor(request => request.Hands)
            .NotNull()
            .NotEmpty()
            .WithMessage("Cannot have a null or empty list of hands.")
            .ForEach(hand => hand
                .NotNull()
                .NotEmpty()
                .WithMessage("Canoot have a null or empty hand.")
                .ForEach(card => card
                    .NotNull()
                    .NotEmpty()
                    .Must(value => Enum.TryParse<Card>(value, out _))
                    .WithMessage("Cannot parse into a card.")));
    }
    
}