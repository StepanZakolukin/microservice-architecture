namespace TaskTrackerService.Dal.Models;

public class TaskDal : BaseDalModel<Guid>
{
    public TaskDal()
    {
        Id = Guid.NewGuid();
    }
    
    public bool Completed { get; set; } = false;
    public required string Title { get; set; }
    public required string? Description { get; set; }
    public required Guid CreatorId { get; init; }
    public required DateTime? Deadline { get; set; }
    public int Number { get; internal set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public Guid PriorityId { get; set; }

    public required PriorityDal? Priority { get; set; }

    public Guid ColumnId { get; set; }

    public ColumnDal Column { get; internal set; } //TODO: поработать над целостностью данных

    public void Update(string title, string? description, bool completed, DateTime? deadline, PriorityDal? priority)
    {
        Title = title;
        Description = description;
        Completed = completed;
        Deadline = deadline;
        Priority = priority;
    }
}