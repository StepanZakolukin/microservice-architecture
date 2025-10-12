using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class ColumnConfiguration : IEntityTypeConfiguration<ColumnDal>
{
    public void Configure(EntityTypeBuilder<ColumnDal> builder)
    {
        builder.ToTable("Column");
        builder.HasMany(column => column.Tasks)
            .WithOne()
            .HasForeignKey(task => task.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}