using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

internal class BoardEditorConfiguration : IEntityTypeConfiguration<BoardEditorDal>
{
    public void Configure(EntityTypeBuilder<BoardEditorDal> builder)
    {
        builder.ToTable("BoardEditor");
        builder.HasOne(editor => editor.Board)
            .WithMany(board => board.Editors)
            .HasForeignKey(editor => editor.BoardId);
    }
}