using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class ColumnConfiguration : IEntityTypeConfiguration<ColumnDal>
{
    public void Configure(EntityTypeBuilder<ColumnDal> builder)
    {
        builder.ToTable("column");
        builder.HasOne(column => column.Tasks)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}