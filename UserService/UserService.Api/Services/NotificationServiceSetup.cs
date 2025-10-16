using UserService.Api.Hubs;
using UserService.Api.Services.Interfaces;

namespace UserService.Api.Services;

public static class NotificationServiceSetup
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            });
        });
        services.AddSignalR();
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }

    public static WebApplication UseNotifications(this WebApplication builder)
    {
        builder.UseCors("AllowAll");
        builder.MapHub<NotificationHub>("/hubs/notifications");
        
        return builder;
    } 
}