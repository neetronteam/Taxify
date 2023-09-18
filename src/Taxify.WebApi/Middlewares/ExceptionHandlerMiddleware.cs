using Taxify.Service.Exceptions;

namespace Taxify.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _request;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _request = request;
        _logger = logger;
    }

    public async ValueTask Invoke(HttpContext context)
    {
        try
        {
            await _request.Invoke(context);
        }
        catch (NotFoundException exception)
        {
            
        }
    }
}