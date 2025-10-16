using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public DateTime CreateAt { get; init; } = DateTime.UtcNow;
    
    private readonly List<Notification> _notifications = [];

    public IEnumerable<Notification> Notifications
    {
        get => _notifications;
        init
        {
            _notifications = value
                .OrderBy(notification => notification.Created)
                .ToList(); 
        }
    }

    public Notification AddNotification(string text)
    {
        var notification = new Notification { User = this, Text = text };
        
        _notifications.Add(notification);

        return notification;
    }
}