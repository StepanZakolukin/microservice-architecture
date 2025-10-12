using Microsoft.Extensions.DependencyInjection;
using TaskTrackerService.Logic.Notification;

namespace TaskTrackerService.Logic;

public static class LogicSetup
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        return services
            .AddScoped<INotificationManager, NotificationManager>();
    }  
}