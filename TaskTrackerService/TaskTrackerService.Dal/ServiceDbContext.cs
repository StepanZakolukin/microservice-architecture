using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal;

public class ServiceDbContext : DbContext
{
    public DbSet<TaskDal> Tasks => Set<TaskDal>();
    public DbSet<BoardDal> Boards => Set<BoardDal>();
    public DbSet<NotificationDal> Notifications => Set<NotificationDal>();

    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ColumnDal>();
        modelBuilder.Entity<SubtaskDal>();
        modelBuilder.Entity<TaskExecutorDal>();
        modelBuilder.HasDefaultSchema("task_tracker_service");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}