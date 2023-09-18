namespace Taxify.Service.Exceptions;

public class AlreadyExistsException:Exception
{
    public AlreadyExistsException(string message):base(message)
    {
        
    }

    public AlreadyExistsException(string message,Exception innerException):base(message,innerException)
    {
        
    }

    public int StatusCode { get; set; } = 403;
}