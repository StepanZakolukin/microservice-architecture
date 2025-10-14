using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskDal>
{
    public void Configure(EntityTypeBuilder<TaskDal> builder)
    {
        builder.ToTable("Task");
        builder
            .HasOne(task => task.Priority)
            .WithMany()
            .HasForeignKey(task => task.PriorityId);
    }
}