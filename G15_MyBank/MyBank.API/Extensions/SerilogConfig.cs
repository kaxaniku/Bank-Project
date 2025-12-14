using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace MyBank.API.Extensions;

internal static class SerilogConfig
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)

            // 🔹 Enrich logs with exception details
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()

            // 🔹 REQUEST LOGS (Information+)
            .WriteTo.File(
                path: "Logs/requests-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} | {Level} | {Message}{NewLine}"
            )

            // 🔹 ERROR LOGS (Error+)
            .WriteTo.File(
                path: "Logs/errors-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error,
                outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} | {Level} | {Message}{NewLine}{Exception}"
            )
        );
    }
}
