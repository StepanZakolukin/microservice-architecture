namespace TaskTrackerService.Dal.Models;

public class NotificationDal : BaseDalModel<Guid>
{
    public required Guid UserId { get; init; }
    public required DateTime Created { get; init; }
    public required string Text { get; init; }
    public bool ReadIt { get; set; } = false;
    
    public NotificationDal()
    {
        Id = Guid.NewGuid();
    }
}