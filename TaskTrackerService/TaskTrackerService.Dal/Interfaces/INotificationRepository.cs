using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Interfaces;

public interface INotificationRepository
{
    void Update(NotificationDal notification);

    Task<IEnumerable<NotificationDal>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken);
    
    Task AddNotificationAsync(NotificationDal notification, CancellationToken cancellationToken);

    Task<NotificationDal?> GetNotificationAsync(Guid notificationId, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}