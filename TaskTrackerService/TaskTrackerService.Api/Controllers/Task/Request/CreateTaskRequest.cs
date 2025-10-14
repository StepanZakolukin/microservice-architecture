namespace TaskTrackerService.Api.Controllers.Task.Request;

public record CreateTaskRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public DateTime? Deadline { get; init; }
    public required Guid ColumnId { get; init; }
    public Guid? PriorityId { get; init; }
}