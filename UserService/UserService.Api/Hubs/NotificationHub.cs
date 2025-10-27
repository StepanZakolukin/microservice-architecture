using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using UserService.Api.Services.Interfaces;
using UserService.Application.Notification;

namespace UserService.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly IUserContext _userContext;
    private readonly INotificationManager _notificationManager;
    private readonly INotificationService _notificationService;

    public NotificationHub(
        INotificationManager notificationManager,
        INotificationService notificationService,
        IUserContext userContext)
    {
        _userContext = userContext;
        _notificationManager = notificationManager;
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = $"{_userContext.UserId}";
        
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            var guidUserId = _userContext.UserId;
            var notificationListResult = await _notificationManager.GetUnreadNotificationListAsync(guidUserId);
            if (notificationListResult.IsFailed) 
                throw new Exception("Авторизированный пользователь почему-то не был найден в системе");
            
            foreach (var notification in notificationListResult.Value.Select(notification => notification.Text))
                await _notificationService.SendNotificationAsync(userId, notification);
        }
    }
}