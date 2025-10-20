using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

internal class BoardConfiguration : IEntityTypeConfiguration<BoardDal>
{
    public void Configure(EntityTypeBuilder<BoardDal> builder)
    {
        builder.ToTable("Board");
        builder.HasMany(board => board.Columns)
            .WithOne(column => column.Board)
            .HasForeignKey(column => column.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(board => board.Editors)
            .WithOne(editor => editor.Board)
            .OnDelete(DeleteBehavior.Cascade);
    }
}