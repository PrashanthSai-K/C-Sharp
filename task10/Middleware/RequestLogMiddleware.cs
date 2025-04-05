using System;

namespace task10.Middleware;

public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLogMiddleware> _logger;

    public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"📨 Incomig request {context.Request.Method} {context.Request.Path}");
        await _next(context);
    }
}
