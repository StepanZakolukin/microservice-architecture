using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTrackerService.Dal;

public static class DalSetup
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfigurationRoot configuration)
    {
        return services.AddDbContext<ServiceDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}