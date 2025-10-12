namespace TaskTrackerService.Api.Controllers.Notification;

public record NotificationRequest
{
    public required Guid UserId { get; init; }
    
    public required string Text { get; init; }
}