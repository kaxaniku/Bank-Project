namespace Products.API.Middlewares;
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Logic before next middleware
        Console.WriteLine("Before next middleware");

        await _next(context);  // Call the next middleware

        // Logic after next middleware
        Console.WriteLine("After next middleware");
    }
}
