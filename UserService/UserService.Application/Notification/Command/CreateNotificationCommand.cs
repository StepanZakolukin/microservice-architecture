namespace UserService.Application.Notification.Command;

public record CreateNotificationCommand
{
    public required string Text { get; init; }
    public required Guid UserId { get; init; }
    public required Guid AuthenticatedUserId { get; init; }
}