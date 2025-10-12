using Microsoft.AspNetCore.SignalR;
using TaskTrackerService.Api.Hubs;
using TaskTrackerService.Api.Services.Interfaces;

namespace TaskTrackerService.Api.Services;

internal class NotificationService : INotificationService
{
    private const string Method = "SendNotification";
    
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string userId, string message, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.Group(userId).SendAsync(Method, message, cancellationToken);
    }
}