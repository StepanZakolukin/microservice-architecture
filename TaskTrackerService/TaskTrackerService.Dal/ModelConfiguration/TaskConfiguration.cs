using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskDal>
{
    public void Configure(EntityTypeBuilder<TaskDal> builder)
    {
        builder.ToTable("Task");
        builder.HasMany(task => task.Executors)
            .WithOne()
            .HasForeignKey(executor => executor.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(task => task.Subtasks)
            .WithOne()
            .HasForeignKey(subtask => subtask.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}