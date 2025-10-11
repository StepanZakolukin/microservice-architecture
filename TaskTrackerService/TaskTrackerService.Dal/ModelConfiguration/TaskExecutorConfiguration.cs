using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TaskExecutorConfiguration : IEntityTypeConfiguration<TaskExecutorDal>
{
    public void Configure(EntityTypeBuilder<TaskExecutorDal> builder)
    {
        builder.ToTable("task_executor");
    }
}