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
    public Guid PriorityId { get; private set; } = Guid.Empty;

    private PriorityDal? _priority;
    public required PriorityDal? Priority
    {
        get => _priority;
        set
        {
            _priority = value;
            PriorityId = value?.Id ?? Guid.Empty;
        }
    }
    
    public Guid ColumnId { get; private set; }

    private ColumnDal _column;
    public ColumnDal Column
    {
        get => _column;
        internal set
        {
            _column = value;
            ColumnId = value?.Id ?? Guid.Empty;
        }
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