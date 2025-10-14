using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class TeamConfiguration : IEntityTypeConfiguration<TeamDal>
{
    public void Configure(EntityTypeBuilder<TeamDal> builder)
    {
        builder.ToTable("Team");
        builder
            .HasMany(team => team.Teammates)
            .WithMany();
        builder.HasOne(team => team.Board)
            .WithOne(board => board.Team)
            .HasForeignKey<TeamDal>(team => team.BoardId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}