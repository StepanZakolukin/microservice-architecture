using FluentResults;

namespace TaskTrackerService.Logic.Notification;

public interface INotificationManager
{
    Task<Guid> CreateNotificationAsync(Guid userId, string message, CancellationToken cancellationToken);

    Task<Result> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
    
    Task<IEnumerable<NotificationDto>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<IEnumerable<string>> GetUnreadNotificationListAsync(Guid userId, CancellationToken cancellationToken = default);
}