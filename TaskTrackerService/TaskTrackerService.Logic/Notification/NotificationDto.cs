namespace TaskTrackerService.Logic.Notification;

public record NotificationDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required DateTime Created { get; init; }
    public required string Text { get; init; }
    public bool ReadIt { get; init; } = false;
}