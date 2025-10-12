using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class BoardConfiguration : IEntityTypeConfiguration<BoardDal>
{
    public void Configure(EntityTypeBuilder<BoardDal> builder)
    {
        builder.ToTable("Board");
        builder.HasMany(board => board.Columns)
            .WithOne()
            .HasForeignKey(column => column.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}