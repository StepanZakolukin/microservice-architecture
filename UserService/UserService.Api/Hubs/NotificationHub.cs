using Microsoft.AspNetCore.SignalR;
using UserService.Api.Services.Interfaces;
using UserService.Application.Notification;

namespace UserService.Api.Hubs;

public class NotificationHub : Hub
{
    private readonly INotificationManager _notificationManager;
    private readonly INotificationService _notificationService;

    public NotificationHub(INotificationManager notificationManager, INotificationService notificationService)
    {
        _notificationManager = notificationManager;
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext().Request.Query["userId"].FirstOrDefault();
        
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            var guidUserId = Guid.Parse(userId);
            var notificationListResult = await _notificationManager.GetUnreadNotificationListAsync(guidUserId);
            if (notificationListResult.IsFailed)
            {
                //TODO: обработать как-то ошибку
                return;
            }
            
            foreach (var notification in notificationListResult.Value.Select(notification => notification.Text))
                await _notificationService.SendNotificationAsync(userId, notification);
        }
    }
}