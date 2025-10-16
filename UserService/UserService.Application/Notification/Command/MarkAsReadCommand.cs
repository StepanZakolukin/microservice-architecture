namespace UserService.Application.Notification.Command;

public record MarkAsReadCommand
{
    public required Guid NotificationId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}