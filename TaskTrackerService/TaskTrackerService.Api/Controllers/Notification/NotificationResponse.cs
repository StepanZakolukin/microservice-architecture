namespace TaskTrackerService.Api.Controllers.Notification;

public class NotificationResponse
{
    public required Guid Id { get; init; }
    
    public required string Text { get; init; }

    public required bool ReadIt { get; init; }
}