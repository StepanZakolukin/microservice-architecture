using Microsoft.AspNetCore.SignalR;
using UserService.Api.Hubs;
using UserService.Api.Services.Interfaces;

namespace UserService.Api.Services;

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