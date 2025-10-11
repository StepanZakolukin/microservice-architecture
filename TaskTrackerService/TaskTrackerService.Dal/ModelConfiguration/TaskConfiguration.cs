using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskDal>
{
    public void Configure(EntityTypeBuilder<TaskDal> builder)
    {
        builder.ToTable("task");
        builder.HasOne(card => card.Executors)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(card => card.Subtasks)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}