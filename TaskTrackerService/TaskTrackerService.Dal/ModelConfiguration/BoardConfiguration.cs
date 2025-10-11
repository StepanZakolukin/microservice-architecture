using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class BoardConfiguration : IEntityTypeConfiguration<BoardDal>
{
    public void Configure(EntityTypeBuilder<BoardDal> builder)
    {
        builder.ToTable("board");
        builder.HasOne(project => project.Columns)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}