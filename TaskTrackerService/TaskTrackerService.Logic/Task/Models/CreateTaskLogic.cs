namespace TaskTrackerService.Logic.Task.Models;

public record CreateTaskLogic
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Guid CreatorId { get; init; }
    public required DateTime? Deadline { get; init; }
    public required Guid ColumnId { get; init; }

    public required Guid PriorityId { get; init; }
}