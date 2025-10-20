using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Auth;
using UserService.Application.Auth.Interfaces;
using UserService.Application.Notification;
using UserService.Application.User;

namespace UserService.Application;

public static class ApplicationSetup
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthManager, AuthManager>()
            .AddScoped<IUserManager, UserManager>()
            .AddScoped<IAccessTokenGenerator, AccessTokenGenerator>()
            .AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>()
            .AddScoped<INotificationManager, NotificationManager>();
        
        return services;
    }
}