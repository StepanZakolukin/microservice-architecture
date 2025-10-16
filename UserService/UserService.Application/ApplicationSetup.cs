using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Auth.Interfaces;
using UserService.Application.Auth.Services;
using UserService.Application.Notification;

namespace UserService.Application;

public static class ApplicationSetup
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccessTokenGenerator, AccessTokenGenerator>()
            .AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>()
            .AddScoped<INotificationManager, NotificationManager>();
        
        return services;
    }
}