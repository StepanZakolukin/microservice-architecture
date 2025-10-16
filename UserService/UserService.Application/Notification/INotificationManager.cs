using FluentResults;
using UserService.Application.Notification.Command;

namespace UserService.Application.Notification;

public interface INotificationManager
{
    Task<Result<Guid>> CreateNotificationAsync(CreateNotificationCommand command, CancellationToken cancellationToken);
    
    Task<Result> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<Domain.Entities.Notification>>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<Domain.Entities.Notification>>> GetUnreadNotificationListAsync(Guid userId, CancellationToken cancellationToken = default);
}