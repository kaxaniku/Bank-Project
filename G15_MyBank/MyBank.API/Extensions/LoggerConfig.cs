using Serilog;
using Serilog.Exceptions;

namespace MyBank.API.Extensions;

internal static class LoggerConfig
{
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .CreateBootstrapLogger();
    }
}
