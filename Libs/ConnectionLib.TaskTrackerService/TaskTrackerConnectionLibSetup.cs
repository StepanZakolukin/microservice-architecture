using ConnectionLib.TaskTrackerService.Board;

namespace ConnectionLib.TaskTrackerService;
using Microsoft.Extensions.DependencyInjection;

public static class TaskTrackerConnectionLibSetup
{
    public static IServiceCollection AddTaskTrackerConnectionLib(this IServiceCollection services)
    {
        services
            .AddScoped<IBoardConnection, BoardConnection>();
        
        return services;
    }
}