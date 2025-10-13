using Serilog;

namespace Products.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
        LoggerInitialization();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Log.Information("Handling HTTP Request...");
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, "An unhandled exception has occurred while executing the request.");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error");
            Log.Error("Failed.");
            return;
        }
        Log.Information("Success.");
    }

    static void LoggerInitialization()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("Logs/ExceptionLogger.txt")
            .CreateLogger();
    }
}
