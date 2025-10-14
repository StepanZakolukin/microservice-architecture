using Microsoft.Extensions.DependencyInjection;
using СonnectionLib.UserService.User;

namespace СonnectionLib.UserService;

public static class UserConnectionLibSetup
{
    public static IServiceCollection AddUserConnectionLib(this IServiceCollection services)
    {
        services.AddScoped<IUserConnection, UserConnection>();
        
        return services;
    }
}