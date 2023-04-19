using System.Diagnostics;

namespace webapi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Handling request: {0} {1}", context.Request.Method, context.Request.Path);

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            await _next(context);

            _logger.LogInformation("Request handled successfully: {0} {1} - StatusCode: {2} - Elapsed: {3}ms", context.Request.Method, context.Request.Path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Request failed: {0} {1} - Exception: {2}", context.Request.Method, context.Request.Path, ex.Message);

            throw;
        }
    }
}