using FluentResults;

namespace UserService.Application.Notification;

public interface INotificationManager
{
    Task<Result<Guid>> CreateNotificationAsync(string text, Guid userId, Guid authenticatedUserId, CancellationToken cancellationToken);
    
    Task<Result> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<Domain.Entities.Notification>>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<Domain.Entities.Notification>>> GetUnreadNotificationListAsync(Guid userId, CancellationToken cancellationToken = default);
}