using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal;

public class ServiceDbContext : DbContext
{
    public DbSet<TaskDal> Tasks => Set<TaskDal>();
    public DbSet<BoardDal> Boards => Set<BoardDal>();
    public DbSet<ColumnDal> Columns => Set<ColumnDal>();
    public DbSet<PriorityDal> Priorities => Set<PriorityDal>();

    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<BoardEditorDal>();
        modelBuilder.HasDefaultSchema("task_tracker_service");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}