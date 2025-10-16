using IdentityService.Api.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces;
using UserService.Application.Services;

namespace UserService.Application;

public static class ApplicationSetup
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccessTokenGenerator, AccessTokenGenerator>()
            .AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        
        return services;
    }
}