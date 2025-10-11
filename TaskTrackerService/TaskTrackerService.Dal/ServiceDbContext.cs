using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal;

public class ServiceDbContext : DbContext
{
    public DbSet<Task> Tasks => Set<Task>();
    public DbSet<BoardDal> Boards => Set<BoardDal>();
    public DbSet<NotificationDal> Columns => Set<NotificationDal>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("task_tracker_service");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}