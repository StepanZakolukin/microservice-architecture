using Microsoft.Extensions.DependencyInjection;
using TaskTrackerService.Logic.Priority;
using TaskTrackerService.Logic.Task;

namespace TaskTrackerService.Logic;

public static class LogicSetup
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddScoped<ITaskManager, TaskManager>();
        
        return services;
    }  
}