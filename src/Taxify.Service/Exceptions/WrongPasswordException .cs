namespace Taxify.Service.Exceptions;

public class WrongPasswordException : Exception
{
    public WrongPasswordException(string message) : base(message)
    { }

    public WrongPasswordException(string message, Exception exception) : base(message, exception)
    { }
}
