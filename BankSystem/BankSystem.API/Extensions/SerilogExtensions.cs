using Serilog;

namespace BankSystem.API.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
    }
}
