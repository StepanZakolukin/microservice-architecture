using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class PriorityConfiguration : IEntityTypeConfiguration<PriorityDal>
{
    public void Configure(EntityTypeBuilder<PriorityDal> builder)
    {
        builder.ToTable("Priority");
    }
}