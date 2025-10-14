using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

internal class ColumnConfiguration : IEntityTypeConfiguration<ColumnDal>
{
    public void Configure(EntityTypeBuilder<ColumnDal> builder)
    {
        builder.ToTable("Column");
        builder.HasMany(column => column.Tasks)
            .WithOne(task => task.Column)
            .HasForeignKey(task => task.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}