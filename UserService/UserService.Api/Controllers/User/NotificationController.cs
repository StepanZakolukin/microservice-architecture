using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Controllers.User.Request;
using UserService.Api.Controllers.User.Response;
using UserService.Api.Services.Interfaces;
using UserService.Application.Notification;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers.User;

[Authorize]
[ApiController]
[Route("api/users")]
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
    /// Получить список уведомлений пользователя
    /// </summary>
    [HttpGet("{user-id:guid}/notifications")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<IEnumerable<NotificationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationListAsync(
        [FromRoute(Name ="user-id")] Guid userId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        if (userContext.UserId != userId)
            return Forbid();
        
        var result = (await _notificationManager.GetNotificationListAsync(userId, cancellationToken))
            .Map(notificationList => notificationList.Select(ConvertToDto));
        
        return result.ToActionResult();
    }

    private NotificationResponse ConvertToDto(Notification notification)
    {
        return new NotificationResponse
        {
            Created = notification.Created,
            Text = notification.Text,
            ReadIt = notification.ReadIt,
            Id = notification.Id,
        };
    }

    /// <summary>
    /// Создать уведомление
    /// </summary>
    [HttpPost("{user-id:guid}/notifications")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateNotificationAsync(
        [FromRoute(Name ="user-id")] Guid userId,
        [FromServices] IUserContext userContext,
        [FromBody] NotificationRequest notification,
        CancellationToken cancellationToken)
    {
        var result = await _notificationManager.CreateNotificationAsync(
            notification.Text,
            userId, userContext.UserId,
            cancellationToken);
        
        await _notificationService.SendNotificationAsync(
            $"{userId}",
            notification.Text,
            cancellationToken);

        return result.ToActionResult();
    }
    
    /// <summary>
    /// Отметить уведомление как прочитанное
    /// </summary>
    [HttpPost("{user-id:guid}/notifications/{notification-id:guid}/mark-as-read")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> MarkAsRead(
        [FromRoute(Name ="user-id")] Guid userId,
        [FromRoute(Name = "notification-id")] Guid notificationId,
        [FromServices] IUserContext userContext,
        CancellationToken cancellationToken)
    {
        if (userContext.UserId != userId)
            return Forbid();
        
        var result = await _notificationManager.MarkAsReadAsync(notificationId, cancellationToken);
        
        return result.ToActionResult();
    }
}