using System;

namespace task10.Middleware;

public class ResponseLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResponseLogMiddleware> _logger;

    public ResponseLogMiddleware(RequestDelegate next, ILogger<ResponseLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        _logger.LogInformation($"✅ Request processed {context.Request.Method} {context.Request.Path}");
    }

}
