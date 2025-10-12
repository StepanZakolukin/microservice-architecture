using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class SubtaskConfiguration : IEntityTypeConfiguration<SubtaskDal>
{
    public void Configure(EntityTypeBuilder<SubtaskDal> builder)
    {
        builder.ToTable("Subtask");
    }
}