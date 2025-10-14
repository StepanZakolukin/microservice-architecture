namespace TaskTrackerService.Dal.Models;

public class TaskDal : BaseDalModel<Guid>
{
    public bool Completed { get; set; } = false;
    public required string Title { get; set; }
    public required string? Description { get; set; }
    public required Guid CreatorId { get; init; }
    public required DateTime? Deadline { get; set; }
    public required Guid ColumnId { get; set; }
    public int Number { get; set; } = 0;
    public Guid PriorityId { get; set; } = Guid.Empty;
    public required PriorityDal? Priority { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public TaskDal()
    {
        Id = Guid.NewGuid();
    }

    public void Update(string title, string? description, bool completed, DateTime? deadline, PriorityDal? priority)
    {
        Title = title;
        Description = description;
        Completed = completed;
        Deadline = deadline;
        Priority = priority;
        PriorityId = priority?.Id ?? Guid.Empty;
    }
}