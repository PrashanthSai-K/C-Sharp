using System;
using Microsoft.AspNetCore.Diagnostics;

namespace task10.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"{exception.Message} : Unhandled exception occured");

        httpContext.Response.StatusCode = 500;
        var erroResponse = new
        {
            Message = "Some internal error occured",
            detail = exception.Message
        };

        await httpContext.Response.WriteAsJsonAsync(erroResponse, cancellationToken);

        return true;
    }
}
