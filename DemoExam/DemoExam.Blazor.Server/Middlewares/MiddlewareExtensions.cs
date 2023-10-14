namespace DemoExam.Blazor.Server.Middlewares;

public static class MiddlewareExtensions
{
    public static WebApplication UseGlobalExceptionCatcher(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<GlobalExceptionCatcher>>();
        app.UseMiddleware<GlobalExceptionCatcher>(logger);
        return app;
    }
}