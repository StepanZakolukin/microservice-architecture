using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Repositories;

namespace TaskTrackerService.Dal;

public static class DalSetup
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<ServiceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services
            .AddScoped<IColumnRepository, ColumnRepository>()
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IPriorityRepository, PriorityRepository>()
            .AddScoped<ITeamRepository, TeamRepository>();
        
        return services;
    }
}