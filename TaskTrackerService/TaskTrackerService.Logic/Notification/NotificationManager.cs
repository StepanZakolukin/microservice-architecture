using Core.Errors;
using FluentResults;
using TaskTrackerService.Dal.Interfaces;
using TaskTrackerService.Dal.Models;

namespace TaskTrackerService.Logic.Notification;

internal class NotificationManager : INotificationManager
{
    private readonly Serilog.ILogger _logger;
    private readonly INotificationRepository _repository;
    
    public NotificationManager(Serilog.ILogger logger, INotificationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Guid> CreateNotificationAsync(Guid userId, string message, CancellationToken cancellationToken)
    {
        var notification = new NotificationDal
        {
            UserId = userId,
            Created = DateTime.UtcNow,
            Text = message
        };
        
        await _repository.AddNotificationAsync(notification, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.Information("Создано уведомление: {@notification}", notification);
        
        return notification.Id;
    }

    public async Task<Result> MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        var notification = await _repository.GetNotificationAsync(notificationId, cancellationToken);
        if (notification is null)
        {
            _logger.Information($"Уведомление с Id = {notificationId} не найдено");
            return Result.Fail(AppError.NotFound);
        }
        notification.ReadIt = true;
        await _repository.SaveChangesAsync(cancellationToken);
        _logger.Information("Уведомление с Id = {@notificationId} помечено как прочитанное", notificationId);
        return Result.Ok();
    }

    public async Task<IEnumerable<NotificationDto>> GetNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _repository.GetNotificationListAsync(userId, cancellationToken);
        return result.Select(notification => new NotificationDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Created = notification.Created,
            Text = notification.Text,
        });
    }

    public async Task<IEnumerable<string>> GetUnreadNotificationListAsync(Guid userId, CancellationToken cancellationToken)
    {
        var notificationList = await _repository.GetNotificationListAsync(
            userId,
            cancellationToken);

        return notificationList
            .Where(notification => !notification.ReadIt)
            .OrderBy(notification => notification.Created)
            .Select(notification => notification.Text);
    }
}