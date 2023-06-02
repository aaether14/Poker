using FluentValidation;
using Poker.Contracts;
using Poker.Domain;

namespace Poker.HandsApi.Validators;

public class DescribeHandRequestValidator : AbstractValidator<DescribeHandRequest>
{

    public DescribeHandRequestValidator()
    {
        RuleFor(request => request.Hand)
            .NotNull()
            .NotEmpty()
            .WithMessage("Canoot have a null or empty hand.")
            .ForEach(card => card
                    .NotNull()
                    .NotEmpty()
                    .Must(value => Enum.TryParse<Card>(value, out _))
                    .WithMessage("Cannot parse into a card."));
    }

}