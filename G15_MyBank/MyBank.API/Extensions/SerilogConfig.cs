using Serilog;
using Serilog.Exceptions;

namespace MyBank.API.Extensions;

internal static class SerilogConfig
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSerilog((services, lc) => lc
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithExceptionDetails());
    }
}
