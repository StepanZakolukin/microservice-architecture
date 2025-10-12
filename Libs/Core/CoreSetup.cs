using Core.Logs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Core;

public static class CoreSetup
{
    public static IServiceCollection AddCore(this IServiceCollection services, IHostBuilder hostBuilder)
    {
        services
            .AddLoggerServices();
        
        hostBuilder.UseSerilog(
            (builderContext, logConfiguration) => logConfiguration.GetConfiguration(),
            preserveStaticLogger: true);
        
        return services;
    } 
}