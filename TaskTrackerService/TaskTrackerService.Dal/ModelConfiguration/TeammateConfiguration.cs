using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TeammateConfiguration : IEntityTypeConfiguration<TeammateDal>
{
    public void Configure(EntityTypeBuilder<TeammateDal> builder)
    {
        builder.ToTable("Teammate");
    }
}