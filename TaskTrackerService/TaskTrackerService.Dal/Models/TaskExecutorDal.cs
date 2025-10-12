namespace TaskTrackerService.Dal.Models;

public class TaskExecutorDal : BaseDalModel<Guid>
{
    public required Guid UserId { get; init; }
    public required InteractionType Type { get; set; }
    public Guid TaskId { get; init; }
    
    public TaskExecutorDal()
    {
        Id = Guid.NewGuid();
    }
}