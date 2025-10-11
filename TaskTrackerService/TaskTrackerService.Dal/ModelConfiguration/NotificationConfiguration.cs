using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.ModelConfiguration;

public class NotificationConfiguration : IEntityTypeConfiguration<NotificationDal>
{
    public void Configure(EntityTypeBuilder<NotificationDal> builder)
    {
        builder.ToTable("notification");
    }
}