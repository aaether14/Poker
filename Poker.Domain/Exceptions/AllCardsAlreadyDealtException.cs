namespace Poker.Domain.Exceptions;

public class AllCardsAlreadyDealtException : DomainException
{

    public AllCardsAlreadyDealtException(string message) : base(message) {}

}