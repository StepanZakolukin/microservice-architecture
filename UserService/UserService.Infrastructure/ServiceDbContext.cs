using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infrastructure;

public class ServiceDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    
    public DbSet<UserRole> Roles => Set<UserRole>();
    
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("user_service");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}