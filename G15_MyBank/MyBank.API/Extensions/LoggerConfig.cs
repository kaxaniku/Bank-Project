using Serilog;
using Serilog.Exceptions;

namespace MyBank.API.Extensions;

internal static class LoggerConfig
{
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithExceptionDetails()
            .CreateLogger();
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }
}
