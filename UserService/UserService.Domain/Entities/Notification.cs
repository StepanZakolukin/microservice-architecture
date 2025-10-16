using Core.Domain.Entities.Base;
using Destructurama.Attributed;

namespace UserService.Domain.Entities;

public class Notification : BaseEntity<Guid>
{
    public Guid UserId => User.Id;
    public User User { get;  internal init; } //TODO: Поработать над целостностью

    public DateTime Created { get; private set; } = DateTime.UtcNow;
    
    [LogAsScalar]
    public required string Text { get; init; }
    
    public bool ReadIt { get; private set; }

    public void MarkAsRead()
    {
        ReadIt = true;
    }

    public Notification()
    {
        Id = Guid.NewGuid();
    }
}