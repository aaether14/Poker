namespace Poker.Domain.Exceptions;

public class InvalidHandException : DomainException
{

    public InvalidHandException(string message) : base(message) {}

}