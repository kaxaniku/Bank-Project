using Serilog;
using Serilog.Events;

namespace MyBank.API.Serilog
{
    public class SerilogConfiguration
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File("logs/requests-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo.File("logs/errors-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
