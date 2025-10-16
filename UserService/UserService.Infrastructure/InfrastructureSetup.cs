using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure;

public static class InfrastructureSetup
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ServiceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        return services;
    }
}