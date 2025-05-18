using Serilog;

namespace ECommerceBackendSystem.API.Extensions;

public static class CustomSeriLogExtensions
{
    public static void UseCustomSeriLog(this IHostBuilder host)
    {
        host.UseSerilog((context, services, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
        );
    }
}
