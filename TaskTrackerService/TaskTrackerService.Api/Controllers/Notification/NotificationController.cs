using Core.Errors;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Api.Services.Interfaces;
using TaskTrackerService.Logic.Notification;

namespace TaskTrackerService.Api.Controllers.Notification;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationManager _notificationManager;
    private readonly INotificationService _notificationService;
    
    public NotificationController(INotificationManager notificationManager, INotificationService notificationService)
    {
        _notificationManager = notificationManager;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Создать уведомление
    /// </summary>
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNotificationAsync(
        [FromBody] NotificationRequest notification,
        CancellationToken cancellationToken)
    {
        var result = await _notificationManager.CreateNotificationAsync(
            notification.UserId,
            notification.Text,
            cancellationToken);
        
        await _notificationService.SendNotificationAsync(
            $"{notification.UserId}",
            notification.Text,
            cancellationToken);

        return Created("api/notifications", result);
    }
    
    /// <summary>
    /// Отметить уведомление как прочитанное
    /// </summary>
    [HttpPut("{notification-id:guid}/mark-as-read")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead([FromRoute(Name = "notification-id")] Guid id, CancellationToken cancellationToken)
    {
        var result = await _notificationManager.MarkAsReadAsync(id, cancellationToken);
        return result.ToActionResult();
    }
}