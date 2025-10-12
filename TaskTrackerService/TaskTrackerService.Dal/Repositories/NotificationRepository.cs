using Microsoft.EntityFrameworkCore;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Dal.Repositories;

internal class NotificationRepository : INotificationRepository
{
    private readonly ServiceDbContext _dbContext;

    public NotificationRepository(ServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(NotificationDal notification)
    {
        _dbContext.Notifications.Update(notification);
    }

    public async Task<IEnumerable<NotificationDal>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        IQueryable<NotificationDal> notifications = _dbContext.Notifications;
        return await notifications
            .Where(notification => notification.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddNotificationAsync(NotificationDal notification, CancellationToken cancellationToken)
    {
        await _dbContext.Notifications.AddAsync(notification, cancellationToken);
    }

    public async Task<NotificationDal?> GetNotificationAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        IQueryable<NotificationDal> notifications = _dbContext.Notifications;
        return await notifications.FirstOrDefaultAsync(notification => notification.Id == notificationId);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}