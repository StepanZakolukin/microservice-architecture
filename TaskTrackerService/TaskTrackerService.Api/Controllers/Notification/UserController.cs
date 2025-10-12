using Microsoft.AspNetCore.Mvc;
using TaskTrackerService.Logic.Notification;

namespace TaskTrackerService.Api.Controllers.Notification;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly INotificationManager _notificationManager;

    public UserController(INotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
    }

    /// <summary>
    /// Получить список уведомлений пользователя
    /// </summary>
    [HttpGet("{user-id:guid}/notifications")]
    [ProducesResponseType<IEnumerable<NotificationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationListAsync([FromRoute(Name = "user-id")] Guid userId, CancellationToken cancellationToken)
    {
        var result = (await _notificationManager.GetNotificationListAsync(userId, cancellationToken))
            .Select(notification => new NotificationResponse
            {
                Text = notification.Text,
                ReadIt = notification.ReadIt,
                Id = notification.Id,
            });
        
        return Ok(result);
    }
}