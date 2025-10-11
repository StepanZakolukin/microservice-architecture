namespace TaskTrackerService.Dal.Models;

public class TaskDal : BaseDalModel<Guid>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Guid CreatorId { get; init; }
    public required Guid ColumnId { get; set; }
    public required int SequenceNumber { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public ICollection<TaskExecutorDal> Executors { get; init; } = new List<TaskExecutorDal>();
    public ICollection<SubtaskDal> Subtasks { get; init; } = new List<SubtaskDal>();
    
    public TaskDal()
    {
        Id = Guid.NewGuid();
    }
}