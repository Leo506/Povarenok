using System.Net;

namespace DemoExam.Blazor.Server.Middlewares;

public class GlobalExceptionCatcher
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionCatcher> _logger;

    public GlobalExceptionCatcher(RequestDelegate next, ILogger<GlobalExceptionCatcher> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured while processing a request");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}